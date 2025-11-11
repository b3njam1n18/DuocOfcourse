using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Core.IRepositories;
using Api.Core.IServices;
using Api.Core.Models;
using Api.Repository.Repositories;
using Microsoft.AspNetCore.Authentication;

namespace Api.Service.Service
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IAuthCredentialsRepository _authCredentialsRepository;

        public UsersService(IUsersRepository usersRepository, IAuthCredentialsRepository authCredentialsRepository)
        {
            _usersRepository = usersRepository;
            _authCredentialsRepository = authCredentialsRepository;
        }

        public async Task<IEnumerable<Users>> GetAllAsync() => await _usersRepository.GetAllAsync();

        public async Task<Users> GetByIdAsync(long id) => await _usersRepository.GetByIdAsync(id);

        public async Task<Users> CreateAsync(Users user)
        {
            user.CreatedAt = DateTime.UtcNow;
            var createdUser = await _usersRepository.CreateAsync(user);
            return createdUser;
        }

        public async Task<Users> UpdateAsync(Users user) => await _usersRepository.UpdateAsync(user);

        public async Task<bool> DeleteAsync(long id) => await _usersRepository.DeleteAsync(id);

        public async Task<Users> GetEmailUser(String email) => await _usersRepository.GetByEmailAsync(email);
    }
}