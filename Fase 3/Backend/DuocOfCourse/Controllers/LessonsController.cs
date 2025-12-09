using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.Core.Models;
using Api.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/courses/{courseId:long}/lessons")]
    public class LessonsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public LessonsController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET api/courses/{courseId}/lessons/{lessonId}
        [HttpGet("{lessonId:long}")]
        public async Task<ActionResult<Lessons>> GetLesson(long courseId, long lessonId)
        {
            // Garantizamos que la lección pertenece al curso correcto
            var lesson = await _context.Lessons
                .Join(
                    _context.Course_Modules,
                    l => l.ModuleId,
                    m => m.Id,
                    (l, m) => new { Lesson = l, Module = m }
                )
                .Where(x => x.Lesson.Id == lessonId && x.Module.CourseId == courseId)
                .Select(x => x.Lesson)
                .FirstOrDefaultAsync();

            if (lesson == null)
                return NotFound();

            return Ok(lesson);
        }

        [HttpPost("{lessonId:long}/video")]
        [RequestSizeLimit(1024L * 1024L * 1024L)] // hasta 1 GB
                                                  // [Authorize] // opcional, si quieres exigir login explícitamente
        public async Task<ActionResult> UploadLessonVideo(
    long courseId,
    long lessonId,
    IFormFile video,
    [FromForm] string? titulo,
    [FromForm] string? descripcion
)
        {
            if (video == null || video.Length == 0)
            {
                return BadRequest("No se recibió archivo de video.");
            }

            // 1) Validar que la lección pertenece al curso
            var lessonJoin = await _context.Lessons
                .Join(
                    _context.Course_Modules,
                    l => l.ModuleId,
                    m => m.Id,
                    (l, m) => new { Lesson = l, Module = m }
                )
                .Where(x => x.Lesson.Id == lessonId && x.Module.CourseId == courseId)
                .FirstOrDefaultAsync();

            if (lessonJoin == null)
                return NotFound("No se encontró la lección para este curso.");

            var lesson = lessonJoin.Lesson;

            // 2) Resolver el usuario que sube el video desde los claims
            long uploaderId;

            // Intentar varios tipos de claim posibles para el ID
            var userIdClaim = User.Claims.FirstOrDefault(c =>
                c.Type == "id" ||
                c.Type == "userId" ||
                c.Type == ClaimTypes.NameIdentifier ||
                c.Type == JwtRegisteredClaimNames.Sub
            );

            if (userIdClaim != null && long.TryParse(userIdClaim.Value, out var parsedId))
            {
                uploaderId = parsedId;
            }
            else
            {
                // Si no hay ID numérico, intentamos por email
                var emailClaim = User.Claims.FirstOrDefault(c =>
                    c.Type == ClaimTypes.Email ||
                    c.Type == JwtRegisteredClaimNames.Email ||
                    c.Type == "email"
                );

                if (emailClaim == null)
                {
                    return Unauthorized("No se pudo identificar al usuario autenticado (sin claim de ID ni de email).");
                }

                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == emailClaim.Value);

                if (user == null)
                {
                    return Unauthorized("El usuario del token no existe en la base de datos.");
                }

                uploaderId = user.Id;
            }

            // 3) Guardar el archivo físico
            var webRoot = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var uploadsRoot = Path.Combine(
                webRoot,
                "media",
                "courses",
                courseId.ToString(),
                "lessons",
                lessonId.ToString()
            );

            Directory.CreateDirectory(uploadsRoot);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(video.FileName)}";
            var filePath = Path.Combine(uploadsRoot, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await video.CopyToAsync(stream);
            }

            // Ruta relativa y URL pública
            var relativePath = Path.Combine(
                "media",
                "courses",
                courseId.ToString(),
                "lessons",
                lessonId.ToString(),
                fileName
            ).Replace("\\", "/");

            var publicUrl = $"{Request.Scheme}://{Request.Host}/{relativePath}";

            // Pequeño checksum opcional (si tu modelo Media lo tiene)
            // Si no existe la columna, puedes quitar checksum_sha256 sin drama
            var checksum = ""; // o calcula un hash si quieres

            var media = new Media
            {
                CourseId = courseId,
                Title = string.IsNullOrWhiteSpace(titulo) ? video.FileName : titulo,
                MimeType = video.ContentType,
                StoragePath = publicUrl,
                SizeBytes = video.Length,
                UploadedBy = uploaderId,
                ChecksumSha256 = checksum
            };

            _context.Media.Add(media);

            // 4) Actualizar la URL del contenido de la lección
            lesson.ContentUrl = publicUrl;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                lessonId = lesson.Id,
                lessonTitle = lesson.Title,
                contentUrl = lesson.ContentUrl,
                mediaId = media.Id
            });
        }

    }
}
