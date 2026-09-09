using Nexora.Api.Models;

namespace Nexora.Api.Mapping
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
