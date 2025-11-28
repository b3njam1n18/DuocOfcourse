using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.DTO
{
    public class CreateQuestion
    {
        public string Prompt { get; set; } = string.Empty;     
        public string Type { get; set; } = "SINGLE";          
        public decimal Points { get; set; } = 1.00m;
        public int Position { get; set; } = 1;
    }
}
