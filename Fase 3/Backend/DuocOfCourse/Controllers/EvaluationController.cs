using Api.Core.DTO;
using Api.Core.IServices;
using Api.Core.Models;
using Api.Service.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DuocOfCourse.Controllers
{
    [ApiController]
    [Route("api/courses/{courseId}/evaluations")]
    public class EvaluationsController : ControllerBase
    {
        private readonly IEvaluationService _service;

        public EvaluationsController(IEvaluationService service)
        {
            _service = service;
        }

        //este es el que permite que el profe cree la evaluacion asociada a un cursos 
        [HttpPost]
        public async Task<IActionResult> CreateEvaluation(long courseId, [FromBody] CreateEvaluationRequest request)
        {
            try
            {
                var result = await _service.CreateEvaluationAsync(courseId, request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("{evaluationId}/questions")]
        public async Task<IActionResult> CreateQuestion(long evaluationId, [FromBody] CreateQuestion dto)
        {
            var result = await _service.CreateQuestionAsync(evaluationId, dto);
            return Ok(result);
        }

        [HttpPost("CreateOption")]
        public async Task<IActionResult> CreateOption(long questionId, [FromBody] CreateOption dto)
        {
            var result = await _service.CreateOptionAsync(questionId, dto);
            return Ok(result);
        }

        [HttpGet("{evaluationId}")]
        public async Task<IActionResult> GetEvaluationFull(long evaluationId)
        {
            try
            {
                var result = await _service.GetEvaluationFullAsync(evaluationId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpPut("{evaluationId}")]
        public async Task<IActionResult> UpdateEvaluation(long evaluationId, [FromBody] UpdateEvaluationDto dto)
        {
            try
            {
                var result = await _service.UpdateEvaluationAsync(evaluationId, dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT /api/courses/{courseId}/evaluations/questions/{questionId}
        [HttpPut("questions/{questionId}")]
        public async Task<IActionResult> UpdateQuestion(long questionId, [FromBody] CreateQuestion dto)
        {
            try
            {
                var updated = await _service.UpdateQuestionAsync(questionId, dto);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT /api/courses/{courseId}/evaluations/options/{optionId}
        [HttpPut("options/{optionId}")]
        public async Task<IActionResult> UpdateOption(long optionId, [FromBody] UpdateOption dto)
        {
            try
            {
                var result = await _service.UpdateOptionAsync(optionId, dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE /api/courses/{courseId}/evaluations/questions/{questionId}
        [HttpDelete("questions/{questionId}")]
        public async Task<IActionResult> DeleteQuestion(long questionId)
        {
            try
            {
                await _service.DeleteQuestionAsync(questionId);
                return Ok(new { message = "Question deleted successfully." });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // DELETE /api/courses/{courseId}/evaluations/options/{optionId}
        [HttpDelete("options/{optionId}")]
        public async Task<IActionResult> DeleteOption(long optionId)
        {
            try
            {
                await _service.DeleteOptionAsync(optionId);
                return Ok(new { message = "Option deleted successfully." });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }


        [HttpPost("Crear_Intento_Evaluacion")]
        public async Task<IActionResult> CreateAttempt(long evaluationId, [FromBody] CreateAttempt dto)
        {
            var attempt = await _service.CreateAttemptAsync(evaluationId, dto);
            return Ok(attempt);
        }

        [HttpPost("EntregarRespuesta")]
        public async Task<IActionResult> SubmitAnswers(long attemptId, [FromBody] SubmitAnswersRequest request)
        {
            var result = await _service.SubmitAnswersAsync(attemptId, request);
            return Ok(result);
        }

        
        [HttpPut("attempts/{attemptId:long}/finish")]
        public async Task<IActionResult> FinishAttempt(long attemptId)
        {
            try
            {
                var result = await _service.FinishAttemptAsync(attemptId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("attempts/{attemptId:long}")]
        public async Task<IActionResult> GetAttemptResult(long attemptId)
        {
            try
            {
                var result = await _service.GetAttemptResultAsync(attemptId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // GET /api/courses/{courseId}/evaluations/{evaluationId}/attempts/by-student?studentId=123
        [HttpGet("{evaluationId:long}/attempts/by-student")]
        public async Task<IActionResult> GetLastAttemptByStudent(long evaluationId, [FromQuery] long studentId)
        {
            if (studentId <= 0)
                return BadRequest(new { message = "studentId es requerido" });

            try
            {
                var result = await _service.GetLastAttemptByStudentAsync(evaluationId, studentId);

                if (result == null)
                    return NotFound(new { message = "El alumno aún no ha rendido esta evaluación." });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPut("{evaluationId}/publish")]
        public async Task<IActionResult> PublishEvaluation(long evaluationId, [FromBody] PublishEvaluationDto dto)
        {
            try
            {
                var result = await _service.PublishEvaluationAsync(evaluationId, dto.Publish);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("/api/courses/{courseId}/lessons/{lessonId}/evaluations")]
        public async Task<IActionResult> GetEvaluationsByLesson(long courseId, long lessonId, [FromQuery] bool? published)
        {
            var result = await _service.GetEvaluationsByLessonAsync(courseId, lessonId, published);
            return Ok(result);
        }


        [HttpGet("/api/courses/{courseId}/evaluations")]
        public async Task<IActionResult> GetEvaluations(long courseId, [FromQuery] bool? published)
        {
            var result = await _service.GetEvaluationsByCourseAsync(courseId, published);
            return Ok(result);
        }

        /*[HttpPost]  verificar 
        public async Task<IActionResult> CreateEvaluation([FromBody] CreateEvaluationDto dto)
        {
            var result = await _service.CreateEvaluationAsync(dto);
            return Ok(result);
        }*/
    }
}
