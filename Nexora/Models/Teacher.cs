using System.Text.Json.Serialization;

namespace Nexora.Api.Models;

public class Teacher
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    [JsonIgnore]
    public ICollection<Course> Courses { get; set; } = [];
}