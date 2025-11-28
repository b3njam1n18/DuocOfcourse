using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Models
{
    public class Course_Categories
    {
        public long Id { get; set; }
        public long SchoolId { get; set; }
        public string Name { get; set; } = string.Empty;

        public Schools School { get; set; }

        public ICollection<Courses> Courses { get; set; }
    }
}
