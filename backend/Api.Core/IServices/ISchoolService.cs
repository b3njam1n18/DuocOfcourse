using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Core.Models;

namespace Api.Core.IServices
{
    public interface ISchoolService
    {
        public Task<List<Schools>> GetAllSchoolsAsync();
    }
}
