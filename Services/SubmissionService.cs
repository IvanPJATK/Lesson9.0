using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class SubmissionService
    {
        private readonly UniversityTasksDbContext _context;
        public SubmissionService(UniversityTasksDbContext context)
        {
            _context = context;
        }
        public async Task<(bool Sucess, string ErrorMessage, SubmissionDto? Data)> CustomerSubmissionAsync(CreateSubmissionDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.RepositoryURL))
            {
                return (false, "Repository URL cannot be blank", null);
            }
            var student = await _context.Students
                .Include(s => s.Enrollments)
                .FirstOrDefaultAsync(s => s.StudentId == dto.StudentId);

            if (student == null)
            {
                return (false, "Student does not exist", null);
            }
            if(!student.IsActive)
            {
                return (false, "Student is not active", null); 
            }
            var assignment = await _context.Assignments.FindAsync(dto.AssignmentId);
            if (assignment == null)
            {
                return (false, "Assignment does not exist", null);
            }
            if(!assignment.IsPublished)
            {
                return (false, "Assignment is not published", null);
            }
            var enrollment = _context.Enrollments.FirstOrDefault(e => e.CourseId == assignment.CourseId);
            if(enrollment == null)
            {
                return (false, "Student must be enrolled in this corse", null);
            }
            string chosen_status = DateTime.UtcNow > assignment.DueDate ? "Laye" : "Submitted";
            var newSubmission = new Submission
            {
                AssignmentId = dto.AssignmentId,
                StudentId = dto.StudentId,
                RepositoryUrl = dto.RepositoryURL,
                SubmittedAt = DateTime.Now,
                Status = chosen_status,
                Score = null, 
                Feedback = null,
            };
            _context.Submissions.Add(newSubmission);
            await _context.SaveChangesAsync();

            var resultDto = new SubmissionDto
            {
                Id = newSubmission.SubmissionId,
                AssignmentId = newSubmission.AssignmentId,
                StudentId = newSubmission.StudentId,
                RepositoryURL = newSubmission.RepositoryUrl,
                Status = newSubmission.Status,
                Score = newSubmission.Score,
                Feedback = newSubmission.Feedback,
            };

            return (true, String.Empty, resultDto);
        }
    }
}
