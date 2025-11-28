using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Models
{
    public class Lesson_Progress
    {
        public long Id { get; set; }
        public long EnrollmentId { get; set; }
        public long LessonId { get; set; }
        public DateTime? CompletedAt { get; set; }

        public Enrollments Enrollment { get; set; }
        public Lessons Lesson { get; set; }

    }
}
