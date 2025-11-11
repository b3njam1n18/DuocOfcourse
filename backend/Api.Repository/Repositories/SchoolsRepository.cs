using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Core.Models;
using Api.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Api.Core.IRepositories;

namespace Api.Repository.Repositories
{

    
    public class SchoolsRepository : ISchoolsRepository
    {
        protected readonly AppDbContext _context;

        public SchoolsRepository (AppDbContext context)
        { _context =  context; }
        public async Task<List<Schools>>ShowSchools()
        {

            return await _context.Schools
                .Include(s => s.CourseCategories)
                .ToListAsync();
        }

    }
}
