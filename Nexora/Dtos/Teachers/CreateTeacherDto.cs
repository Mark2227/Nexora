using System.ComponentModel.DataAnnotations;
namespace Nexora.Api.Dtos.Teachers;

public class CreateTeacherDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
}
