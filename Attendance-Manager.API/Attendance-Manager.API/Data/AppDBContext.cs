using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Attendance_Manager.API.JwtAuthentication;

namespace Attendance_Manager.API.Data
{
    public class AppDBContext : IdentityDbContext<ApplicationUser>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Class> classes { get; set; }
        public DbSet<Enrollment> enrollments { get; set; }
        public DbSet<Session> sessions { get; set; }
        public DbSet<AttendanceRecord> attendanceRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users", "dbo");

                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId)
                .HasColumnName("user_id");

                entity.Property(e => e.UserName)
                .HasColumnName("user_name")
                .HasMaxLength(50);

                entity.Property(e => e.Email)
                .HasColumnName("email")
                .HasMaxLength(50);

                entity.Property(e => e.Password)
                .HasColumnName("password")
                .HasMaxLength(50); 

                entity.Property(e => e.Role)
                .HasColumnName("role")
                .HasMaxLength(50);

                entity.Property(e => e.IdentityUserID)
                .HasColumnName("identityuser_id")
                .HasMaxLength(450);
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("Classes", "dbo");

                entity.HasKey(e => e.ClassId);

                entity.Property(e => e.ClassId)
                .HasColumnName("class_id");

                entity.Property(e => e.ClassName)
                .HasColumnName("class_name")
                .HasMaxLength(50);

                entity.Property(e => e.TeacherId)
                .HasColumnName("teacher_id");

            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.ToTable("Enrollments", "dbo");

                entity.HasKey(e => new {e.ClassId, e.StudentId});

                entity.Property(e => e.ClassId)
                .HasColumnName("class_id");

                entity.Property(e => e.StudentId)
                .HasColumnName("student_id");

            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.ToTable("Sessions", "dbo");

                entity.HasKey(e => e.SessionId);

                entity.Property(e => e.SessionId);

                entity.Property(e => e.SessionName)
                .HasColumnName("session_name")
                .HasMaxLength(50);

                entity.Property(e => e.ClassId)
                .HasColumnName("class_id");

                entity.Property(e => e.SessionSchedule)
                .HasColumnName("session_schedule");

            });

            modelBuilder.Entity<AttendanceRecord>(entity =>
            {
                entity.ToTable("AttendanceRecords", "dbo");
             
                entity.HasKey(e => e.AttendanceId);
                
                entity.Property(e => e.SessionId)
                .HasColumnName("session_id");
                
                entity.Property(e => e.StudentId)
                .HasColumnName("student_id");
                
                entity.Property(e => e.status)
                .HasColumnName("status");

            });
        }
    }
}
