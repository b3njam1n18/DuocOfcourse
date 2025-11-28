using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text;
using System.Threading.Tasks;
using Api.Core.DTO;
using Api.Core.IRepositories;
using Api.Core.IServices;
using Api.Core.Models;
using Api.Repository.Repositories;
using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens;
using static Org.BouncyCastle.Math.EC.ECCurve;


namespace Api.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordResetRepository _passwordResetRepository;
        private readonly IEmailService _emailService;
        private readonly IAuthCredentialsRepository _authCredentialsRepository;
        private readonly IConfiguration _config;

        public AuthService(
            IUsersRepository usersRepository,
            IPasswordResetRepository passwordResetRepository,
            IEmailService emailService,
            IAuthCredentialsRepository authCredentialsRepository,
            IConfiguration config)
        {
            _usersRepository = usersRepository;
            _passwordResetRepository = passwordResetRepository;
            _emailService = emailService;
            _authCredentialsRepository = authCredentialsRepository;
            _config = config;
        }

        public async Task RequestPasswordResetAsync(string email)
        {
            var user = await _usersRepository.GetByEmailAsync(email);
            if (user == null) return; // No revelamos si existe o no

            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            var expires = DateTime.UtcNow.AddHours(1);

            await _passwordResetRepository.CreateAsync(new Password_Reset_Token
            {
                UserId = user.Id,
                Token = token,
                ExpiresAt = expires
            });

            var resetLink = $"https://frontend.duocofcourse.cl/reset-password?token={token}";


            //formato que le llega al correo
            await _emailService.SendAsync(
                user.Email,
                "Restablecer tu contraseña",
                $"Hola {user.FirstName},<br/><br/>" +
                $"Haz clic en el siguiente enlace para restablecer tu contraseña:<br/>" +
                $"<a href='{resetLink}'>Restablecer contraseña</a><br/><br/>" +
                $"Este enlace expirará en 1 hora."
            );
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            // 1. Buscar usuario
            var user = await _authCredentialsRepository.GetUserByEmailAsync(request.Email);
            if (user == null)
                throw new Exception("Usuario o contraseña inválidos.");

            // 2. Buscar credenciales
            var cred = await _authCredentialsRepository.GetCredentialsByUserIdAsync(user.Id);
            if (cred == null)
                throw new Exception("Credenciales no encontradas.");

            // 3. Validar contraseña
            bool isValid = BCrypt.Net.BCrypt.Verify(request.Password, cred.PasswordHash);

            if (!isValid)
                throw new Exception("Usuario o contraseña inválidos.");

            // 4. Generar token JWT
            var token = GenerateJwtToken(user);

            return new LoginResponse
            {
                UserId = user.Id,
                Email = user.Email,
                Token = token,
                RoleId = user.RoleId
            };
        }

        // Método para generar token JWT
        private string GenerateJwtToken(Users user)
        {
            var secret = _config["Jwt:Key"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim("userId", user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.RoleId.ToString())
        };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(6),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> ResetPasswordAsync(string token, string newPassword)
        {
            //buscar token
            var reset = await _passwordResetRepository.GetByTokenAsync(token);

            //se Valida existencia y expiración
            if (reset == null)
                throw new ArgumentException("Token inválido.");

            // Comparar usando hora local del servidor (no UTC), sino manda error en API
            if (reset.ExpiresAt < DateTime.Now)
                throw new ArgumentException("El token ha expirado. Por favor reintentar.");

            // Verificar si ya se usó
            if (reset.IsUsed)
                throw new InvalidOperationException("El token ya fue utilizado.");

            // Cargar usuario y credenciales
            var user = reset.User;
            if (user == null)
                throw new InvalidOperationException("No se encontró el usuario asociado al token.");

            var cred = user.AuthCredentials?.FirstOrDefault();
            if (cred == null)
                throw new InvalidOperationException("El usuario no tiene credenciales asociadas.");

            // Actualizar contraseña con hash seguro
            cred.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            cred.PasswordUpdatedAt = DateTime.Now;

            // Marcar token como usado
            await _passwordResetRepository.InvalidateTokenAsync(token);

            // Confirmación por correo
            await _emailService.SendAsync(
                user.Email,
                "Tu contraseña fue actualizada",
                $"Hola {user.FirstName},<br/><br/>" +
                $"Tu contraseña ha sido restablecida correctamente el {DateTime.Now:dd/MM/yyyy HH:mm}.<br/><br/>" +
                $"Si no realizaste este cambio, contacta al soporte de inmediato."
            );

            return true;
        }
    }
}
