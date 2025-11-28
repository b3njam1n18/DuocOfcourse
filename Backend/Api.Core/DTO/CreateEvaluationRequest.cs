using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.DTO
{
    public class CreateEvaluationRequest
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DueAt { get; set; }
        public string Type { get; set; } = "QUIZ";  
        public decimal PassThreshold { get; set; } = 0.60m;
    }
}
