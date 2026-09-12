using System.Text.Json.Serialization;

namespace Nexora.Api.Models;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    [JsonIgnore]
    public ICollection<Enrollment> Enrollments { get; set; } = [];
}