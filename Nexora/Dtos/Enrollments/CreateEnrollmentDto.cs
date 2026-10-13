using System.ComponentModel.DataAnnotations;

namespace Nexora.Api.Dtos.Enrollments;

public class CreateEnrollmentDto
{
    [Range(1, int.MaxValue)]
    public int StudentId { get; set; }

    [Range(1, int.MaxValue)]
    public int CourseId { get; set; }
}
