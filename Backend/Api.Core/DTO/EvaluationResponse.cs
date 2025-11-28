using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.DTO
{
    public class EvaluationResponse
    {
        public long Id { get; set; }
        public long CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DueAt { get; set; }
        public string Type { get; set; } = string.Empty;
        public decimal PassThreshold { get; set; }
        public bool IsFinalExam { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
