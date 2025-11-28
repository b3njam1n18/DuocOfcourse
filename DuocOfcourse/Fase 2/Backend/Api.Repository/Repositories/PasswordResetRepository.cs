using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Core.IRepositories;
using Api.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository.Repositories
{
    public class PasswordResetRepository : IPasswordResetRepository
    {
        private readonly AppDbContext _context;

        public PasswordResetRepository(AppDbContext context)
        {
            _context = context;
        }
        /// Obtiene un token válido y su usuario asociado (con AuthCredentials)
        public async Task<Password_Reset_Token?> GetByTokenAsync(string token)
        {

            return await _context.Password_Reset_Tokens
                .Include(p => p.User)
                //carga el usuario asociado 
                .ThenInclude(u => u.AuthCredentials) 
                //carga de coleccion de credenciales
                .FirstOrDefaultAsync(p => p.Token == token && !p.IsUsed);
        }
        /// Crea un nuevo token de restablecimiento de contraseña.
        public async Task CreateAsync(Password_Reset_Token token)
        {
            // Elimina tokens anteriores del mismo usuario
            var existingTokens = await _context.Password_Reset_Tokens
                .Where(p => p.UserId == token.UserId && !p.IsUsed)
                .ToListAsync();

            if (existingTokens.Any())
            {
                _context.Password_Reset_Tokens.RemoveRange(existingTokens);
            }

            await _context.Password_Reset_Tokens.AddAsync(token);
            await _context.SaveChangesAsync();
        }
        /// Marca el token como usado (no reutilizable)
        public async Task InvalidateTokenAsync(string token)
        {
            var reset = await _context.Password_Reset_Tokens
                .FirstOrDefaultAsync(p => p.Token == token);
            if (reset != null)
            {
                reset.IsUsed = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}