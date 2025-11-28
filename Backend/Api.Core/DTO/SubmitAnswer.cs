using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.DTO
{
    public class SubmitAnswer
    {
        public long QuestionId { get; set; }
        public long? OptionId { get; set; }   
        public string? OpenText { get; set; } 
    }
}
