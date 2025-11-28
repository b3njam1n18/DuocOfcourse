using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Models
{
    public class ModuleResponse
    {
        public long Id { get; set; }
        public long CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Position { get; set; }
    }

}
