using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Models
{
    public class Options
    {
        public long Id { get; set; }
        public long QuestionId { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; } = false;

        public Questions Question { get; set; }
        public ICollection<Answers> Answers { get; set; }
    }
}
