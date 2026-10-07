using System.ComponentModel.DataAnnotations;

namespace Nexora.Api.Dtos.Enrollments;

public class CreateEnrollmentDto
{
    [Required]
    public int StudentId { get; set; }
    [Required]
    public int CourseId { get; set; }
}
