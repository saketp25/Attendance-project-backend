using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Attendance_Manager.API.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        { }

        public DbSet<Users> users { get; set;  }

        public DbSet<Teachers> teachers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("users", "dbo");

                entity.Property(e => e.UserId)
                .HasColumnName("userId");

                entity.Property(e => e.UserName)
                    .HasColumnName("userName");

            });

            modelBuilder.Entity<Teachers>(entity =>
            {
                entity.ToTable("teachers", "dbo");

                entity.HasKey(e => e.TeacherId);

                entity.Property(e => e.TeacherId)
                .HasColumnName("teacher_id");

                entity.Property(e => e.TeacherName)
                .HasColumnName("teacher_name")
                .HasMaxLength(50);

                entity.Property(e => e.email)
                .HasColumnName("email")
                .HasMaxLength(50);

                entity.Property(e => e.password)
                .HasColumnName("password")
                .HasMaxLength(50);
                
            });
        }
    }
}
