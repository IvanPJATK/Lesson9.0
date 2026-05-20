namespace WebApplication1.Models
{
    public partial class Assignment
    {
        public bool isOverdue(DateTime now)
        {
            return DueDate < now;
        }
    }
}
