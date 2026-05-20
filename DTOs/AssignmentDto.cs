namespace WebApplication1.DTOs
{
    public class AssignmentDto
    {
        public int AssignmentId { get; set; }
        public string Title { get; set; } = null!;
        public DateTime Due_date { get; set; } = DateTime.MinValue;
        public decimal Max_points { get; set; }
        public string Published_status { get; set; } = null!;
        public int SubmissionCount { get; set; }
    }
}
