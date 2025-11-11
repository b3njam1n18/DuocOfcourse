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
    }
}