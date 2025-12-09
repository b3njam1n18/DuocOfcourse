using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Models
{
    public class Roles
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<Users> Users { get; set; }

    }
}
