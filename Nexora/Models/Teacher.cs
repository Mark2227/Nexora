namespace Nexora.Api.Models;

public class Teacher
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<Course> Courses { get; set; } = [];
}