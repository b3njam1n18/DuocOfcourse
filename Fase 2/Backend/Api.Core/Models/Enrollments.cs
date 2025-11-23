using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Models
{
    public class Enrollments
    {
        public long Id { get; set; }
        public long StudentId { get; set; }
        public long CourseId { get; set; }
        public DateTime EnrolledAt { get; set; }
        public string Status { get; set; } = "IN_PROGRESS";

        public Users Student { get; set; }
        public Courses Course { get; set; }

        public ICollection<Course_Progress> CourseProgress { get; set; }
        public Certificates Certificate { get; set; }
        public ICollection<Lesson_Progress> LessonProgress { get; set; }
    }
}
