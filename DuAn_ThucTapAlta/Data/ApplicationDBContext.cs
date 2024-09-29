using Microsoft.EntityFrameworkCore;
using DuAn_ThucTapAlta;
using DuAn_ThucTapAlta.Models;

namespace DuAn_ThucTapAlta.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentVersion> DocumentVersions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<WorkGroup> WorkGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasKey(e => e.UserId);
            modelBuilder.Entity<Flight>().HasKey(e => e.FlightId);
            modelBuilder.Entity<Document>().HasKey(e => e.DocumentId);
            modelBuilder.Entity<DocumentVersion>().HasKey(e => e.VersionId);
            modelBuilder.Entity<Permission>().HasKey(e => e.PermissionId);
            modelBuilder.Entity<WorkGroup>().HasKey(e => e.GroupId);

            //Quan he 1 - N, moi WorkGroup co nhieu User
            modelBuilder.Entity<User>()
                .HasOne<WorkGroup>(e => e.WorkGroup)
                .WithMany(wg => wg.Users)
                .HasForeignKey(e => e.GroupID);

            //Quan he 1 - N, moi Flight co nhieu Document
            modelBuilder.Entity<Document>()
                .HasOne<Flight>(d => d.Flight)
                .WithMany(f => f.Documents)
                .HasForeignKey(d => d.FlightId);

            //Quan he 1 - N, moi Document co nhieu phien ban 
            modelBuilder.Entity<DocumentVersion>()
                .HasOne<Document>(dv => dv.Document)
                .WithMany(d => d.DocumentVersions)
                .HasForeignKey(dv => dv.DocumentId);

            // Quan he N - N, Permission và WorkGroup (mot nhom co nhieu quyen va mot quyen co the thuoc nhieu nhom)
            modelBuilder.Entity<Permission>()
                .HasMany(p => p.WorkGroups)
                .WithMany(wg => wg.Permissions)
                .UsingEntity(j => j.ToTable("WorkGroupPermissions")); // Bảng trung gian cho quan hệ N-N
        }
    }
}
