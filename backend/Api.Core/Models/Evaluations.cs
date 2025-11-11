using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Models
{
    public class Evaluations
    {
        public long Id { get; set; }
        public long CourseId { get; set; }
        public long? LessonId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DueAt { get; set; }
        public string Type { get; set; } = "QUIZ";
        public decimal PassThreshold { get; set; } = 0.6000m;
        public bool IsFinalExam { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Courses Course { get; set; }
        public Lessons? Lesson { get; set; }

        public ICollection<Questions> Questions { get; set; }
        public ICollection<Attempts> Attempts { get; set; }
    }
}
