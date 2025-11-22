using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Attendance_Manager.API.JwtAuthentication;

namespace Attendance_Manager.API.Data
{
    public class AppDBContext : IdentityDbContext<ApplicationUser>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        { }

        public DbSet<Teacher> teachers { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<Class> classes { get; set; }
        public DbSet<ClassStudent> classStudents { get; set; }
        public DbSet<Session> sessions { get; set; }
        public DbSet<AttendanceRecord> attendanceRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("teachers", "dbo");

                entity.HasKey(e => e.TeacherId);

                entity.Property(e => e.TeacherId)
                .HasColumnName("teacher_id");

                entity.Property(e => e.TeacherName)
                .HasColumnName("teacher_name")
                .HasMaxLength(50);

                entity.Property(e => e.TeacherEmail)
                .HasColumnName("email")
                .HasMaxLength(50);

                entity.Property(e => e.TeacherPassword)
                .HasColumnName("password")
                .HasMaxLength(50);

            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Students", "dbo");

                entity.HasKey(e => e.StudentId);

                entity.Property(e => e.StudentId)
                .HasColumnName("student_id");

                entity.Property(e => e.StudentName)
                .HasColumnName("student_name")
                .HasMaxLength(50);

                entity.Property(e => e.StudentEmail)
                .HasColumnName("email")
                .HasMaxLength(50);

                entity.Property(e => e.StudentPassword)
                .HasColumnName("password")
                .HasMaxLength(50);

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

            modelBuilder.Entity<ClassStudent>(entity =>
            {
                entity.ToTable("ClassStudents", "dbo");

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
