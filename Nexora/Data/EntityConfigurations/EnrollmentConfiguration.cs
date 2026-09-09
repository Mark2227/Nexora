using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexora.Api.Models;

namespace Nexora.Api.Data.EntityConfigurations
{
    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            // Composite Primary Key
            builder.HasKey(e => new
            {
                e.StudentId,
                e.CourseId
            });

            // Enrollment → Student
            builder
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId);

            // Enrollment → Course
            builder
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId);
        }
    }
}