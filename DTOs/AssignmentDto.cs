namespace WebApplication1.DTOs
{
    public class AssignmentDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public DateTime Due_date = DateTime.MinValue;
        public decimal Max_points { get; set; }
        public string Published_status { get; set; } = null!;
        public int SubmissionCount { get; set; }
    }
}
