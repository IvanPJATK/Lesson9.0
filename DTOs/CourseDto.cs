namespace WebApplication1.DTOs
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Credits { get; set; }
        public List<AssignmentDto> Assignments { get; set;  } = new List<AssignmentDto>();
    }
}
