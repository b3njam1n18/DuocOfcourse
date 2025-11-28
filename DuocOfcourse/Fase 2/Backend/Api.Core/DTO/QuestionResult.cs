using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.DTO
{
    public class QuestionResult
    {
        public long QuestionId { get; set; }
        public string Prompt { get; set; } = string.Empty;
        public decimal Points { get; set; }

        public List<OptionResult> Options { get; set; } = new();

        public long? SelectedOptionId { get; set; }
        public bool? IsCorrect { get; set; }
        public string? OpenText { get; set; }
    }

}
