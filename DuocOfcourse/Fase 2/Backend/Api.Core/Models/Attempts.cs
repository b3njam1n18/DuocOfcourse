using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Models
{
    public class Attempts
    {
        public long Id { get; set; }
        public long EvaluationId { get; set; }
        public long StudentId { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public decimal? Score { get; set; }

        public Evaluations Evaluation { get; set; }
        public Users Student { get; set; }
        public ICollection<Answers> Answers { get; set; }
    }
}
