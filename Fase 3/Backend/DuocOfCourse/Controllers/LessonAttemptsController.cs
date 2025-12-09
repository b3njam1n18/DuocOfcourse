using System.Threading.Tasks;
using Api.Core.DTO;
using Api.Core.IServices;
using Microsoft.AspNetCore.Mvc;

namespace DuocOfCourse.Controllers
{
    [ApiController]
    [Route("api/courses/{courseId:long}/lessons/{lessonId:long}/attempts")]
    public class LessonAttemptsController : ControllerBase
    {
        private readonly IAttemptService _attemptService;

        public LessonAttemptsController(IAttemptService attemptService)
        {
            _attemptService = attemptService;
        }

        // POST /api/courses/{courseId}/lessons/{lessonId}/attempts
        [HttpPost]
        public async Task<IActionResult> SubmitLessonAttempt(
            long courseId,
            long lessonId,
            [FromBody] SubmitLessonAttemptRequest dto)
        {
            var result = await _attemptService.SubmitLessonAttemptAsync(courseId, lessonId, dto);
            return Ok(result);
        }

        // GET /api/courses/{courseId}/lessons/{lessonId}/attempts?studentId=20
        [HttpGet]
        public async Task<IActionResult> GetLessonAttempts(
            long courseId,
            long lessonId,
            [FromQuery] long studentId)
        {
            if (studentId <= 0)
            {
                return BadRequest(new { message = "studentId es requerido" });
            }

            var attempts = await _attemptService
                .GetLessonAttemptsByStudentAsync(courseId, lessonId, studentId);

            return Ok(attempts);
        }
    }
}
