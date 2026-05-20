using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly UniversityTasksDbContext _context;
        public CoursesController(UniversityTasksDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetCourses([FromQuery] bool activeOnly = true)
        {
            var query = _context.Courses.AsQueryable();
            if (activeOnly)
            {
                query = query.Where(x => x.IsActive);
            }
            var courses = await query
                .Select(c => new CourseDto
                {
                    Id = c.CourseId,
                    Name = c.Name,
                    Code = c.Code,
                    Credits = c.Credits,
                }).ToListAsync();
            return Ok(courses);
        }
        [HttpGet("{idCourse}/assignments")]
        public async Task<IActionResult> GetCourseAssignments(int idCourse, [FromQuery] bool published_only = true)
        {
            var courseExists = await _context.Courses.AnyAsync(c => c.CourseId == idCourse);
            if (!courseExists)
            {
                return NotFound($"Course with ID {idCourse} does not exist");
            }
            var query = _context.Assignments.Where(a => a.CourseId == idCourse);
            if(published_only)
            {
                query = query.Where(a => a.IsPublished);
            }
            var assignments = await query
                .Select(a => new AssignmentDto
                    { 
                        AssignmentId = a.AssignmentId,
                        Title = a.Title,
                        Due_date = a.DueDate,
                        Max_points = a.MaxPoints,
                        Published_status = a.IsPublished ? "Published" : "Draft",
                        SubmissionCount = a.Submissions.Count,
                    }
                ).ToListAsync();
            return Ok(assignments);
        }
    }
}
