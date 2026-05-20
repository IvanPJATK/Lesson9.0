namespace WebApplication1.DTOs
{
    public class StudentDashboardDto
    {
        public int Id { get; set; }
        public string IndexNumber { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public bool IsActive { get; set; }
        public List<StudentDashboardEnrollmentDto> Enrollments { get; set; } = new List<StudentDashboardEnrollmentDto>();
        public List<SubmissionDto> Submissions { get; set; } = new List<SubmissionDto>();
    }
    public class StudentDashboardEnrollmentDto
    {
        public int Id { get; set; }
        public string Status { get; set; } = null!;
        public string CourseName { get; set; } = null!;
    }
}
