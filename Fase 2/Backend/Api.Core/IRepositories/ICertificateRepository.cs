using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Core.Models;

namespace Api.Core.IRepositories
{
    public interface ICertificateRepository
    {
        Task<Certificates> CreateAsync(Certificates cert);
        Task<Certificates?> GetByIdAsync(long id);
    }
}
