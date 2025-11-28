using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Models
{
    public class Course_Progress
    {
        public long Id { get; set; }

        public long EnrollmentId { get; set; }

        public decimal Percentage { get; set; }

        public DateTime LastUpdateAt { get; set; }

        public Enrollments Enrollment { get; set; }
        public Courses Course { get; set; }
    }
}
