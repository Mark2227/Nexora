using System.ComponentModel.DataAnnotations;
namespace Nexora.Api.Dtos.Students;

public class CreateStudentDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
}
