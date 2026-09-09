using Nexora.Api.Models;

namespace Nexora.Api.Mapping
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder
                .HasOne(c => c.Teacher)
                .WithMany(t => t.Courses)
                .HasForeignKey(c => c.TeacherId);
        }
    }
}
