using System.Threading.Tasks;
using Api.Core.DTO;
using Api.Core.IRepositories;
using Api.Core.IServices;
using Api.Core.Models;
using Api.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DuocOfCourse.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IAuthCredentialsRepository _authCredentialsRepository;

        public UsersController(IUsersService usersService, IAuthCredentialsRepository authCredentialsRepository)
        {
            _usersService = usersService;
            _authCredentialsRepository = authCredentialsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _usersService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var user = await _usersService.GetByIdAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        // DuocOfCourse/Controllers/UsersController.cs (método Register)

        [HttpPost("registro")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest("Correo y contraseña son obligatorios.");

            var existingUser = await _usersService.GetEmailUser(request.Email);
            if (existingUser != null)
                return BadRequest("El correo ya está registrado.");

            // 🔹 Crear el nuevo usuario con escuela y carrera
            var newUser = new Users
            {
                RoleId = request.Roleid,
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                SecondLastName = request.SecondLastName,
                Email = request.Email,
                CareerId = request.CareerId,   // 👈 NUEVO
                IsActive = true,
            };

            var createdUser = await _usersService.CreateAsync(newUser);

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var credentials = new Auth_Credentials
            {
                UserId = createdUser.Id,
                PasswordHash = passwordHash,
                EmailVerified = false,
                PasswordUpdatedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _authCredentialsRepository.CreateAsync(credentials);

            return Ok(new
            {
                message = "Usuario registrado exitosamente.",
                user = new
                {
                    createdUser.Id,
                    createdUser.FirstName,
                    createdUser.LastName,
                    createdUser.Email
                }
            });
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] Users user)
        {
            if (id != user.Id) 
                return BadRequest();
            return Ok(await _usersService.UpdateAsync(user));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _usersService.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}