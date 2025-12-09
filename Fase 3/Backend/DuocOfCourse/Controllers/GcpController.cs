using Api.Core.IServices;
using Microsoft.AspNetCore.Mvc;

namespace DuocOfCourse.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GcpController : Controller
    {
        private readonly IGcpService _GcpService;
        public GcpController(IGcpService gcpService)
        {
            _GcpService = gcpService;
        }


        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] IFormFile file, [FromForm] string folder)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Archivo vacío");

            var url = await _GcpService.UploadFileAsync(file, "cursos");

            return Ok(new { url });
        }
    }
}
