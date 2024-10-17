using DuAn_ThucTapAlta.DTO.Users;
using DuAn_ThucTapAlta.Models;
using DuAn_ThucTapAlta.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;

    public AuthController(IUserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
    {
        if (string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
        {
            return BadRequest("Email và mật khẩu là bắt buộc.");
        }

        var user = await _userService.ValidateUserAsync(loginDto.Email, loginDto.Password);

        if (user == null)
        {
            return Unauthorized("Thông tin tài khoản hoặc mật khẩu không chính xác.");
        }

        var role = user.Role.RoleName;

        // Tạo token JWT
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), 
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()), 
            new Claim(ClaimTypes.Role, role) 
        }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"] 
        };

        var token = tokenHandler.CreateToken(tokenDescriptor); 
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new { Token = tokenString }); 
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
    {
        if (string.IsNullOrWhiteSpace(userRegisterDto.Email))
        {
            return BadRequest("Email không được để trống.");
        }

        if (!userRegisterDto.Email.EndsWith("@vietjetair.com"))
        {
            return BadRequest("Email phải có đuôi miền @vietjetair.com.");
        }
        if (string.IsNullOrWhiteSpace(userRegisterDto.PassWord))
        {
            return BadRequest("Mật khẩu không được để trống.");
        }

        if (string.IsNullOrWhiteSpace(userRegisterDto.ConfirmPassWord))
        {
            return BadRequest("Vui lòng nhập lại mật khẩu.");
        }

        if (userRegisterDto.PassWord != userRegisterDto.ConfirmPassWord)
        {
            return BadRequest("Mật khẩu và nhập lại mật khẩu không khớp.");
        }

        var userExists = await _userService.GetUserByEmailAsync(userRegisterDto.Email);
        if (userExists != null)
        {
            return BadRequest("Người dùng đã tồn tại.");
        }

        var newUser = new User
        {
            Email = userRegisterDto.Email,
            PassWord = userRegisterDto.PassWord,
            RoleId = userRegisterDto.RoleId,
            GroupId = userRegisterDto.GroupId
        };

        await _userService.CreateUserAsync(newUser);

        return Ok("Đăng ký thành công!");
    }

}
