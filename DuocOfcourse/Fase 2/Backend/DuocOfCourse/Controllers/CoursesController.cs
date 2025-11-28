using Api.Core.DTO;
using Api.Core.IServices;
using Api.Core.Models;
using Autofac.Core;
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
                await _courseService
                    .DeleteCourseAsync(courseId);
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
                var result = await _courseService
                    .GetTeacherCoursesAsync(teacherId);
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
                var result = await _courseService
                    .GetCourseByIdAsync(courseId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("{courseId}/modules")]
        public async Task<IActionResult> CreateModule(long courseId, [FromBody] CreateModuleRequest dto)
        {
            var module = await _courseService
                .CreateModuleAsync(courseId, dto);
            return Ok(module);
        }

        [HttpPost("modules/{moduleId}/lessons")]
        public async Task<IActionResult> CreateLesson(long moduleId, [FromBody] CreateLessonRequest request)
        {
            try
            {
                var result = await _courseService
                    .CreateLessonAsync(moduleId, request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



        [HttpPost("{courseId}/publicar")]
        public async Task<IActionResult> PublishCourse(long courseId)
        {
            try
            {
                var result = await _courseService
                    .PublishCourseAsync(courseId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("lessons/{lessonId}")]
        public async Task<IActionResult> UpdateLesson(long lessonId, [FromBody] CreateLessonRequest request)
        {
            try
            {
                var result = await _courseService
                    .UpdateLessonAsync(lessonId, request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("lessons/{lessonId}")]
        public async Task<IActionResult> DeleteLesson(long lessonId)
        {
            try
            {
                await _courseService
                    .DeleteLessonAsync(lessonId);
                return Ok(new { message = "eliminado con exito." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("lessons/{lessonId}/media")]
        public async Task<IActionResult> AddMedia(long lessonId, [FromBody] UploadMediaRequest request)
        {
            try
            {
                var result = await _courseService
                    .AddMediaToLessonAsync(lessonId, request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("media/{mediaId}")]
        public async Task<IActionResult> DeleteMedia(long mediaId)
        {
            try
            {
                await _courseService
                    .DeleteMediaAsync(mediaId);
                return Ok(new { message = "video borrado exitosamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

