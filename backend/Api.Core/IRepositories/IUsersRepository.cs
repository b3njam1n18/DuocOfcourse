using Api.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Repository.Repositories
{
    public interface IUsersRepository
    {
        Task<IEnumerable<Users>> GetAllAsync();
        Task<Users> GetByIdAsync(long id);
        Task<Users> CreateAsync(Users user);
        Task<Users> UpdateAsync(Users user);
        Task<bool> DeleteAsync(long id);
        Task<Users?> GetByEmailAsync(string email);
    }
}