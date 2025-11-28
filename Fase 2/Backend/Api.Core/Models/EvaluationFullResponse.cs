using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Core.DTO;

namespace Api.Core.Models
{
    public class EvaluationFullResponse
    {
        public long Id { get; set; }
        public long CourseId { get; set; }
        public long? LessonId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DueAt { get; set; }
        public string Type { get; set; } = string.Empty;
        public decimal PassThreshold { get; set; }
        public bool IsFinalExam { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<QuestionResponse> Questions { get; set; } = new();
    }
}
