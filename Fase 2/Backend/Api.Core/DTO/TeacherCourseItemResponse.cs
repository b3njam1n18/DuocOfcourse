using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.DTO
{
    public class TeacherCourseItemResponse
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? CoverImagePath { get; set; }
        public bool IsPublished { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
