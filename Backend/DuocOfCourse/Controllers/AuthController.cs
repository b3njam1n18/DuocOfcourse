using Api.Core.DTO;
using Api.Core.IServices;
using Autofac.Core;
using Microsoft.AspNetCore.Mvc;

namespace DuocOfCourse.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        //controlador para enviar correo al usuario
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
                return BadRequest("El correo electrónico es obligatorio.");

            try
            {
                await _authService.RequestPasswordResetAsync(request.Email);
                return Ok("Si el correo está registrado, se enviará un enlace para restablecer la contraseña.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ForgotPassword: {ex.Message}");
                return StatusCode(500, "Error interno del servidor al procesar la solicitud.");
            }
        }

        //controlador para reestablecer contraseña
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Token) || string.IsNullOrWhiteSpace(request.NewPassword))
                return BadRequest("El token y la nueva contraseña son obligatorios.");

            try
            {
                var result = await _authService.ResetPasswordAsync(request.Token, request.NewPassword);

                if (!result)
                    return BadRequest("Token inválido o expirado.");

                return Ok("Contraseña actualizada correctamente.");
            }
            catch (ArgumentException ex)
            {
                // Token inválido o expirado
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                //token usado, usuario no encontrado, etc.
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ResetPassword: {ex.Message}");
                return StatusCode(500, "Error interno del servidor al restablecer la contraseña.");
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var response = await _authService.LoginAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }
}
