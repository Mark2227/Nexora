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

            base.OnModelCreating(modelBuilder);
        }
    }
}