using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.DTO
{
    public class QuestionResponse
    {
        public long Id { get; set; }
        public string Prompt { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public decimal Points { get; set; }
        public int Position { get; set; }

        public List<OptionResponse> Options { get; set; } = new();
    }
}
