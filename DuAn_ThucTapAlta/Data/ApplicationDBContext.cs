using Microsoft.EntityFrameworkCore;
using DuAn_ThucTapAlta;
using DuAn_ThucTapAlta.Models;

namespace DuAn_ThucTapAlta.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentVersion> DocumentVersions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<WorkGroup> WorkGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<Role>().HasKey(r => r.RoleId);
            modelBuilder.Entity<Flight>().HasKey(f => f.FlightId);
            modelBuilder.Entity<Document>().HasKey(d => d.DocumentId);
            modelBuilder.Entity<DocumentVersion>().HasKey(dv => dv.VersionId);
            modelBuilder.Entity<Permission>().HasKey(p => p.PermissionId);
            modelBuilder.Entity<WorkGroup>().HasKey(w => w.GroupId);

            //Quan he 1 - N, moi WorkGroup co nhieu User
            modelBuilder.Entity<User>()
                .HasOne<WorkGroup>(e => e.WorkGroup)
                .WithMany(wg => wg.Users)
                .HasForeignKey(e => e.GroupId);

            //Quan he 1 - N, mot Role co nhieu User
            modelBuilder.Entity<User>()
                .HasOne<Role>(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            //Quan he 1 - N, mot User co the tai len nhieu DocumentVersion
            modelBuilder.Entity<DocumentVersion>()
                .HasOne<User>(d => d.User)
                .WithMany(u => u.DocumentVersions)
                .HasForeignKey(d => d.UserId);

            //Quan he 1 - N, moi Flight co nhieu Document
            modelBuilder.Entity<Document>()
                .HasOne<Flight>(d => d.Flight)
                .WithMany(f => f.Documents)
                .HasForeignKey(d => d.FlightId)
                .OnDelete(DeleteBehavior.NoAction);

            //Quan he 1 - N, moi User co the tham gia nhieu chuyen bay
            modelBuilder.Entity<Flight>()
                .HasOne<User>(f => f.User)
                .WithMany(u => u.Flights)
                .HasForeignKey(f => f.UserId);
            //.IsRequired(false);

            //Quan he N - N, moi Document co nhieu phien ban 
            modelBuilder.Entity<DocumentVersion>()
                .HasOne<Document>(dv => dv.Document)
                .WithMany(d => d.DocumentVersions)
                .HasForeignKey(dv => dv.DocumentId);

            //Quan he N - N, Permission và WorkGroup (mot nhom co nhieu quyen va mot quyen co the thuoc nhieu nhom)
            modelBuilder.Entity<Permission>()
                .HasMany(p => p.WorkGroups)
                .WithMany(wg => wg.Permissions)
                .UsingEntity(j => j.ToTable("WorkGroupPermissions")); //Bảng trung gian cho quan hệ N-N

            //Quan he N - N, Document va Permission
            modelBuilder.Entity<Permission>()
                .HasMany<Document>(p => p.Documents)
                .WithMany(d => d.Permissions)
                .UsingEntity(j => j.ToTable("DocumentPermissions")); //Bang trung gian

            //Quan he 1 - N, mot User co the tai len nhieu Document
            modelBuilder.Entity<Document>()
                .HasOne<User>(d => d.User)
                .WithMany(u => u.Documents)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}   
