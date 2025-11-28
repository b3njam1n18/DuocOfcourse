using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Core.Models;

namespace Api.Core.IRepositories
{
    public interface IAuthCredentialsRepository
    {
        Task<Auth_Credentials> CreateAsync(Auth_Credentials entity);
        Task<Auth_Credentials?> GetByUserIdAsync(long userId);
        Task<Users?> GetUserByEmailAsync(string email);
        Task<Auth_Credentials?> GetCredentialsByUserIdAsync(long userId);
    }
}
