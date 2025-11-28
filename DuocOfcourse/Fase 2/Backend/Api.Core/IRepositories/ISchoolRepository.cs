using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Core.Models;

namespace Api.Core.IRepositories
{
    public interface ISchoolsRepository
    {
        Task<List<Schools>> ShowSchools();
    }
}
