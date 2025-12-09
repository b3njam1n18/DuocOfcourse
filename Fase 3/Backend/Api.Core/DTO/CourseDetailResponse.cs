using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.DTO
{
    public class CourseDetailResponse
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? CoverImagePath { get; set; }
        public bool IsPublished { get; set; }
        public long TeacherId { get; set; }
        public long CategoryId { get; set; }
        public long SchoolId { get; set; }
        public string? TeacherFullName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

}
