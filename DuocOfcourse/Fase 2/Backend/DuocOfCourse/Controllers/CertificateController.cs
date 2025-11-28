using Api.Core.DTO;
using Api.Core.IServices;
using Api.Core.Models;
using Api.Service.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost]
        public async Task<IActionResult> GenerateCertificate(long enrollmentId)
        {
            var result = await _service.GenerateCertificateAsync(enrollmentId);
            return Ok(result);
        }

        [HttpGet("{certificateId}/download")]
        public async Task<IActionResult> DownloadCertificate(long certificateId)
        {
            var (file, name) = await _service.DownloadAsync(certificateId);
            return File(file, "application/pdf", name);
        }



        //controlador de prueba
        
            [HttpGet("prueba")]
            public IActionResult Get()
            {
                return Ok(new { message = "pong fro backend" });
            }
        
    }
}
