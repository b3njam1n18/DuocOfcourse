using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Core.IRepositories;
using Api.Core.IServices;
using Api.Core.Models;
using Api.Repository.Repositories;

namespace Api.Service.Service
{
    public class SchoolService : ISchoolService
    {
        private readonly ISchoolsRepository _schoolsRepository;
        public SchoolService(ISchoolsRepository schoolRepository)
        {
            _schoolsRepository = schoolRepository;
        }

        public async Task<List<Schools>> GetAllSchoolsAsync()
        {
            var escuela = await _schoolsRepository.ShowSchools();

            return escuela;
        }

    }
}
