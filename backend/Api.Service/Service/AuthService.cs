using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Api.Core.IRepositories;
using Api.Core.IServices;
using Api.Core.Models;
using Api.Repository.Repositories;
using BCrypt.Net;



namespace Api.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordResetRepository _passwordResetRepository;
        private readonly IEmailService _emailService;

        public AuthService(
            IUsersRepository usersRepository,
            IPasswordResetRepository passwordResetRepository,
            IEmailService emailService)
        {
            _usersRepository = usersRepository;
            _passwordResetRepository = passwordResetRepository;
            _emailService = emailService;
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
