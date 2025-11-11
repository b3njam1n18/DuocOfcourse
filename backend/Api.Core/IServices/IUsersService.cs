using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Core.Models;
using Api.Repository.Repositories;

namespace Api.Core.IServices
{
    public interface IUsersService
    {
        Task<IEnumerable<Users>> GetAllAsync();
        Task<Users> GetByIdAsync(long id);
        Task<Users> CreateAsync(Users user);
        Task<Users> UpdateAsync(Users user);
        Task<bool> DeleteAsync(long id);
        public Task<Users> GetEmailUser(String email);
    }
}