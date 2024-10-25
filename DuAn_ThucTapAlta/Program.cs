using DuAn_ThucTapAlta.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using DuAn_ThucTapAlta.Services;
using Microsoft.OpenApi.Models;
using System.Text;

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container

    // Đọc cấu hình từ file appsettings.json
    builder.Configuration.AddJsonFile("appsettings.json");

    // Configure Database
    builder.Services.AddDbContext<ApplicationDBContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
    var jwtKey = builder.Configuration["Jwt:Key"];
    var key = Encoding.UTF8.GetBytes(jwtKey);

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero
    };

    options.Events = new JwtBearerEvents
    {
        OnChallenge = async context =>
        {
            // Trả về phản hồi lỗi 403 khi người dùng không có quyền truy cập
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = "application/json";

            // Kiểm tra nếu chưa có phản hồi
            if (string.IsNullOrEmpty(context.Error))
            {
                await context.Response.WriteAsync("{\"message\":\"Bạn không có quyền truy cập nội dung này.\"}");
            }
            else
            {
                await context.Response.WriteAsync("{\"message\":\"Token không hợp lệ hoặc hết hạn.\"}");
            }
            context.HandleResponse();
        }
    };
    });

    // Configure Authorization
    builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
            options.AddPolicy("RequireUserRole", policy => policy.RequireRole("Staff"));
            options.AddPolicy("RequireUserRole", policy => policy.RequireRole("Manager"));
            options.AddPolicy("RequireUserRole", policy => policy.RequireRole("Pilot"));
            options.AddPolicy("RequireUserRole", policy => policy.RequireRole("Stewardess"));
        });

    // Configure Swagger
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Vietjetair API", Version = "v1" });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter JWT with Bearer scheme. Example: 'Bearer {token}'",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        });
    });

    // Add application services (Dependency Injection)
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IDocumentService, DocumentService>();
    builder.Services.AddScoped<IWorkGroupService, WorkGroupService>();
    builder.Services.AddScoped<IDocumentVersionService, DocumentVersionService>();
    builder.Services.AddScoped<IPermissionService, PermissionService>();
    builder.Services.AddScoped<IFlightService, FlightService>();
    builder.Services.AddScoped<IRoleService, RoleService>();

    // Add controllers
    builder.Services.AddControllers();

    // Build the app
    var app = builder.Build();

    // Configure the HTTP request pipeline
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseExceptionHandler("/error");

    app.UseHttpsRedirection();

    app.UseRouting();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
