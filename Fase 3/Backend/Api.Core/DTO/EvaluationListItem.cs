using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.DTO
{
    public class EvaluationListItemDto
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsPublished { get; set; }
        public bool IsFinalExam { get; set; }
        public DateTime? DueAt { get; set; }
    }
}
