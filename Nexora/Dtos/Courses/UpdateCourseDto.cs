using System.ComponentModel.DataAnnotations;
namespace Nexora.Api.Dtos.Courses;

public class UpdateCourseDto
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = null!;
    [Range(1, int.MaxValue)]
    public int TeacherId { get; set; }
}
