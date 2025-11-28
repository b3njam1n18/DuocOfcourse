using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Models
{
    public class Questions
    {
        public long Id { get; set; }
        public long EvaluationId { get; set; }
        public string Prompt { get; set; } = string.Empty;
        public string Type { get; set; } = "SINGLE";
        public decimal Points { get; set; } = 1.00m;
        public int Position { get; set; } = 1;

        public Evaluations Evaluation { get; set; }
        public ICollection<Options> Options { get; set; }
        public ICollection<Answers> Answers { get; set; }
    }
}
