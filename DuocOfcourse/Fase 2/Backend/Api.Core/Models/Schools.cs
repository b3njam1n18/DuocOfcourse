using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Models
{
    public class Schools
    { public long Id {  get; set; }
      public string Name { get; set; }

        public ICollection<Course_Categories> CourseCategories { get; set; } = new List<Course_Categories>();

    }
}
