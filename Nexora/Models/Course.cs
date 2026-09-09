namespace Nexora.Api.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = null!;

        public ICollection<Enrollment> Enrollments { get; set; } = [];
    }
}
