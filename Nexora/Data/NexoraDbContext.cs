using Microsoft.EntityFrameworkCore;
using Nexora.Api.Data.EntityConfigurations;
using Nexora.Api.Models;

namespace Nexora.Api.Data
{
    public class NexoraDbContext : DbContext
    {
        public NexoraDbContext(DbContextOptions<NexoraDbContext> options)
            : base(options)
        {
        }

        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TeacherConfiguration());
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new EnrollmentConfiguration());

            // Teachers
            modelBuilder.Entity<Teacher>().HasData(
                new Teacher
                {
                    Id = 1,
                    Name = "Anna"
                },
                new Teacher
                {
                    Id = 2,
                    Name = "David"
                }
            );

            // Courses
            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    Id = 1,
                    Title = "EF Core Basics",
                    TeacherId = 1
                },
                new Course
                {
                    Id = 2,
                    Title = "ASP.NET Core",
                    TeacherId = 1
                },
                new Course
                {
                    Id = 3,
                    Title = "C# Fundamentals",
                    TeacherId = 2
                }
            );

            // Students
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1,
                    Name = "Mark"
                },
                new Student
                {
                    Id = 2,
                    Name = "John"
                },
                new Student
                {
                    Id = 3,
                    Name = "Emily"
                }
            );

            // Enrollments
            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment
                {
                    StudentId = 1,
                    CourseId = 1
                },
                new Enrollment
                {
                    StudentId = 1,
                    CourseId = 2
                },
                new Enrollment
                {
                    StudentId = 2,
                    CourseId = 1
                },
                new Enrollment
                {
                    StudentId = 3,
                    CourseId = 3
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}