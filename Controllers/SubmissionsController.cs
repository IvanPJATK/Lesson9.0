using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubmissionsController : ControllerBase
    {
        private readonly SubmissionService _submissionService;
        public SubmissionsController(SubmissionService submissionService)
        {
            _submissionService = submissionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubmission([FromBody] CreateSubmissionDto dto)
        {
            var result = await _submissionService.CustomerSubmissionAsync(dto);
            if (!result.Sucess) { return BadRequest(result.ErrorMessage); }
            return CreatedAtAction(nameof(StudentsController.GetStudentDashboard),result.Data);
        }
        [HttpPut("{idSubmission}/grade")]
        public async Task<IActionResult> GradeSubmission(int idSubmission, [FromBody] GradeSubmissionDto dto)
        {
            var result = await _submissionService.GradeSubmissionAsync(idSubmission, dto);
            if (!result.Sucess)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result);
        }
        [HttpDelete("{idSubmission}")]
        public async Task<IActionResult> DeleteSubmission(int idSubmission)
        {
            var result = await _submissionService.DeleteSubmissionAsync(idSubmission);
            if(result.StatusCode == 404)
            {
                return NotFound(result.ErorMessage);
            }
            if(result.StatusCode == 400)
            {
                return BadRequest(result.ErorMessage);
            }
            return NoContent();
        }
    }
}
