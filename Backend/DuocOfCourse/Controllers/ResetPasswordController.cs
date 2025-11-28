using Api.Core.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DuocOfCourse.Controllers
{
    [ApiController]
    [Route("reset-password")]
    public class ResetPasswordController : Controller
    {
        private readonly IAuthService _authService;

        public ResetPasswordController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult ShowForm([FromQuery] string token)
        {
            if (string.IsNullOrEmpty(token))
                return Content("<h3 style='color:red;'>Token inválido o ausente.</h3>", "text/html");

            string html = $@"
            <html>
            <head>
                <title>Restablecer Contraseña</title>
                <style>
                    body {{
                        font-family: Arial, sans-serif;
                        background-color: #f4f4f4;
                        display: flex;
                        justify-content: center;
                        align-items: center;
                        height: 100vh;
                    }}
                    .container {{
                        background: white;
                        padding: 40px;
                        border-radius: 8px;
                        box-shadow: 0 0 10px rgba(0,0,0,0.2);
                        text-align: center;
                        width: 350px;
                    }}
                    input {{
                        width: 100%;
                        padding: 10px;
                        margin: 10px 0;
                        border: 1px solid #ccc;
                        border-radius: 5px;
                    }}
                    button {{
                        background-color: #007bff;
                        color: white;
                        border: none;
                        padding: 10px 20px;
                        border-radius: 5px;
                        cursor: pointer;
                    }}
                    button:hover {{ background-color: #0056b3; }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <h2>Restablecer tu contraseña</h2>
                    <form method='post'>
                        <input type='hidden' name='token' value='{token}' />
                        <input type='password' name='newPassword' placeholder='Nueva contraseña' required /><br/>
                        <button type='submit'>Guardar nueva contraseña</button>
                    </form>
                </div>
            </body>
            </html>";
            return Content(html, "text/html");
        }

        [HttpPost]
        public async Task<IActionResult> ProcessReset([FromForm] string token, [FromForm] string newPassword)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(newPassword))
                return Content("<h3 style='color:red;'>Faltan datos.</h3>", "text/html");

            var success = await _authService.ResetPasswordAsync(token, newPassword);

            string message = success
                ? "<h3 style='color:green;'Contraseña actualizada correctamente.</h3>"
                : "<h3 style='color:red;'>Token inválido o expirado.</h3>";

          
            string html = $@"
            <html>
            <head>
                <meta http-equiv='refresh' content='5;url=https://www.duoc.cl'>
                <script>
                    window.history.replaceState(null, '', '/reset-password');
                </script>
            </head>
            <body style='font-family:Arial; text-align:center; margin-top:100px;'>
                {message}
                <p>Serás redirigido en unos segundos...</p>
            </body>
            </html>";

            return Content(html, "text/html");
        }
    }
}
