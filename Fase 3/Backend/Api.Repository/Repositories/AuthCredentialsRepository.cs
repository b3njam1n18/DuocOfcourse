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
    public class AuthCredentialsRepository : IAuthCredentialsRepository
    {
        private readonly AppDbContext _context;

        public AuthCredentialsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Auth_Credentials> CreateAsync(Auth_Credentials entity)
        {
            _context.Auth_Credentials.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Auth_Credentials?> GetByUserIdAsync(long userId)
        {
            return await _context.Auth_Credentials.FirstOrDefaultAsync(a => a.UserId == userId);
        }

        public async Task<Users?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.IsActive);
        }

        public async Task<Auth_Credentials?> GetCredentialsByUserIdAsync(long userId)
        {
            return await _context.Auth_Credentials
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<Password_Reset_Token?> GetActiveTokenByUserIdAsync(long userId)
        {
            return await _context.Password_Reset_Tokens
                .Where(t => t.UserId == userId && !t.IsUsed && t.ExpiresAt > DateTime.UtcNow)
                .OrderByDescending(t => t.Id)
                .FirstOrDefaultAsync();
        }

        public async Task InvalidateTokenAsync(Password_Reset_Token token)
        {
            token.IsUsed = true;
            _context.Password_Reset_Tokens.Update(token);
            await _context.SaveChangesAsync();
        }
    }
}