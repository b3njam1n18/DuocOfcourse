using Api.Core.DTO;
using Api.Core.IServices;
using Api.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace DuocOfCourse.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet] // ← ESTE ES EL NUEVO
        public async Task<IActionResult> GetAllCourses()
        {
            try
            {
                var result = await _courseService.GetAllCoursesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _courseService.CreateCourseAsync(request);
                return CreatedAtAction(nameof(GetCourseById), new { courseId = result.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{courseId}")]
        public async Task<IActionResult> UpdateCourse(long courseId, [FromBody] UpdateCourseRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _courseService.UpdateCourseAsync(courseId, request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{courseId}")]
        public async Task<IActionResult> DeleteCourse(long courseId)
        {
            try
            {
                await _courseService.DeleteCourseAsync(courseId);
                return Ok(new { message = "Curso borrado con exito." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("teacher/{teacherId}")]
        public async Task<IActionResult> GetTeacherCourses(long teacherId)
        {
            try
            {
                var result = await _courseService.GetTeacherCoursesAsync(teacherId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{courseId}")]
        public async Task<IActionResult> GetCourseById(long courseId)
        {
            try
            {
                var result = await _courseService.GetCourseByIdAsync(courseId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("{courseId}/lessons")]
        public async Task<IActionResult> GetLessonsByCourse(long courseId)
        {
            try
            {
                var result = await _courseService.GetLessonsByCourseAsync(courseId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("{courseId}/enroll")]
        public async Task<IActionResult> EnrollStudent(long courseId, [FromBody] EnrollStudentRequest request)
        {
            try
            {
                await _courseService.EnrollStudentAsync(courseId, request.StudentId);
                return Ok(new { message = "Alumno inscrito correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("student/{studentId}/enrollments")]
        public async Task<IActionResult> GetStudentEnrollments(long studentId)
        {
            try
            {
                var courseIds = await _courseService.GetEnrolledCourseIdsByStudentAsync(studentId);
                return Ok(courseIds); // devuelve [1, 3, 5, ...]
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        // POST: api/Courses/{courseId}/modules
        [HttpPost("{courseId:long}/modules")]
        public async Task<IActionResult> CreateModule(long courseId, [FromBody] CreateModuleRequest request)
        {
            try
            {
                var module = await _courseService.CreateModuleAsync(courseId, request);
                return Ok(module);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST: api/Courses/modules/{moduleId}/lessons
        [HttpPost("modules/{moduleId:long}/lessons")]
        public async Task<IActionResult> CreateLesson(long moduleId, [FromBody] CreateLessonRequest request)
        {
            try
            {
                var lesson = await _courseService.CreateLessonAsync(moduleId, request);
                return Ok(lesson);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }




    }
}
