using System.Globalization;
using Api.Core.DTO;
using Api.Core.IServices;
using Microsoft.AspNetCore.Mvc;

namespace DuocOfCourse.Controllers
{
    [ApiController]
    [Route("api/enrollments/certificate")]
    public class CertificateController : ControllerBase
    {
        private readonly ICertificateService _service;

        public CertificateController(ICertificateService service)
        {
            _service = service;
        }

        // 🔹 Endpoint antiguo, por si lo ocupas en otra parte
        [HttpPost]
        public async Task<IActionResult> GenerateCertificate([FromQuery] long enrollmentId)
        {
            var result = await _service.GenerateCertificateAsync(enrollmentId);
            return Ok(result);
        }

        [HttpGet("{certificateId:long}/download")]
        public async Task<IActionResult> DownloadCertificate(long certificateId)
        {
            var (file, name) = await _service.DownloadAsync(certificateId);
            return File(file, "application/pdf", name);
        }

        // 🔹 NUEVO: certificado por curso, usando la nota que viene del front
        [HttpGet("~/api/courses/{courseId:long}/certificate")]
        public async Task<IActionResult> DownloadCourseCertificate(
                long courseId,
                [FromQuery] long studentId,
                [FromQuery] string? finalGrade
            )
                    {
            if (studentId <= 0)
                return BadRequest("Falta studentId.");

            decimal? finalGradeDecimal = null;

            if (!string.IsNullOrWhiteSpace(finalGrade))
            {
                if (decimal.TryParse(
                        finalGrade,
                        NumberStyles.Any,
                        CultureInfo.InvariantCulture,
                        out var parsed))
                {
                    finalGradeDecimal = parsed;
                }
            }

            var pdfBytes = await _service.GenerateCourseCertificatePdfAsync(
                courseId,
                studentId,
                finalGradeDecimal
            );

            var fileName = $"certificado_curso_{courseId}_alumno_{studentId}.pdf";
            return File(pdfBytes, "application/pdf", fileName);
        }

        //controlador de prueba
        [HttpGet("prueba")]
        public IActionResult Get()
        {
            return Ok(new { message = "pong fro backend" });
        }
    }
}
