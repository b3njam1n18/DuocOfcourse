using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Models
{
    public class Courses
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public long TeacherId { get; set; }
        public long CategoryId { get; set; }
        public long SchoolId { get; set; }
        public DateTime? StartsAt { get; set; }
        public DateTime? EndsAt { get; set; }
        public bool IsPublished { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Course_Categories Category { get; set; }
        public Users Teacher { get; set; }
        public Schools School { get; set; }

        public ICollection<Course_Modules> Modules { get; set; }
        public ICollection<Evaluations> Evaluations { get; set; }
        public ICollection<Media> Media { get; set; }
        public ICollection<Enrollments> Enrollments { get; set; }
        public ICollection<Course_Progress> CourseProgress { get; set; }


    }
}
