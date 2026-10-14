namespace Nexora.Api.Dtos.Courses;

public class CourseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public int TeacherId { get; set; }
    public string TeacherName { get; set; } = null!;
}
