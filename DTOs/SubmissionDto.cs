namespace WebApplication1.DTOs
{
    public class SubmissionDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int AssignmentId { get; set; }
        public string RepositoryURL { get; set; } = null!;
        public string Status { get; set; } = null!;
        public int? Score { get; set; }
        public string? Feedback { get; set; }
    }
    public class CreateSubmissionDto
    {
        public int AssignmentId { get; set; }
        public int StudentId { get; set; }
        public string RepositoryURL { get; set; } = null!;
    }

    public class GradeSubmissionDto
    {
        public int Score { get; set; }
        public string Feedback { get; set; } = null!;
    }
}
