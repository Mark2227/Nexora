using Nexora.Api.Models;
using System.Text.Json.Serialization;

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;

    public int TeacherId { get; set; }
    public Teacher Teacher { get; set; } = null!;

    [JsonIgnore]
    public ICollection<Enrollment> Enrollments { get; set; } = [];
}