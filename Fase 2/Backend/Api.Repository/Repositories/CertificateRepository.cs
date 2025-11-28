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
        public class CertificateRepository : ICertificateRepository
        {
            private readonly AppDbContext _context;

            public CertificateRepository(AppDbContext context)
            {
                _context = context;
            }

            public async Task<Certificates> CreateAsync(Certificates cert)
            {
                _context.Certificates.Add(cert);
                await _context.SaveChangesAsync();
                return cert;
            }

            public async Task<Certificates?> GetByIdAsync(long id)
            {
                return await _context.Certificates.FirstOrDefaultAsync(x => x.Id == id);
            }
        }

    }
