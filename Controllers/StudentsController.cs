using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly UniversityTasksDbContext _context;
        public StudentsController(UniversityTasksDbContext context)
        {
            _context = context;
        }
        [HttpGet("{idStudent}/dashboard")]
        public async Task<IActionResult> GetStudentDashboard(int idStudent)
        {
            var dashboard = await _context.Students
                .Where(s => s.StudentId == idStudent)
                .Select(s => new StudentDashboardDto
                {
                    Id = s.StudentId,
                    IndexNumber = s.IndexNumber,
                    FullName = s.FirstName + " " + s.LastName,
                    IsActive = s.IsActive,

                    Enrollments = s.Enrollments.Select(e => new StudentDashboardEnrollmentDto
                    {
                        Id = e.EnrollmentId,
                        Status = e.Status,
                        CourseName = e.Course.Name
                    }
                    ).ToList(),
                    Submissions = s.Submissions.Select(sub => new SubmissionDto
                    { 
                        Id = sub.SubmissionId,
                        StudentId = sub.StudentId,
                        AssignmentId = sub.AssignmentId,
                        RepositoryURL = sub.RepositoryUrl,
                        Status = sub.Status,
                        Score = sub.Score,
                        Feedback = sub.Feedback
                    }
                    ).ToList(),
                })
                .FirstOrDefaultAsync();

            if (dashboard == null)
            {
                return NotFound($"Student {idStudent} was not found");
            }

            return Ok(dashboard);
        }
    }
}
