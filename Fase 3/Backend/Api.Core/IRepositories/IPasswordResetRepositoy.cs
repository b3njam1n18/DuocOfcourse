using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Core.Models;

namespace Api.Core.IRepositories
{
    public interface IPasswordResetRepository
    {
        Task<Password_Reset_Token> GetByTokenAsync(string token);
        Task CreateAsync(Password_Reset_Token token);
        Task InvalidateTokenAsync(string token);




    }
}
