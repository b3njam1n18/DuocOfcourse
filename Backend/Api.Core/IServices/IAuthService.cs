using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Core.DTO;

namespace Api.Core.IServices
{
    public interface IAuthService
    {
        Task RequestPasswordResetAsync(string email);
        Task<bool> ResetPasswordAsync(string token, string newPassword);
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
