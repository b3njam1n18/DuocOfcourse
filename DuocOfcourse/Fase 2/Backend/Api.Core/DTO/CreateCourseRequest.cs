using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.DTO
{
    public class CreateCourseRequest
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public long TeacherId { get; set; }
        public long CategoryId { get; set; }
        public long SchoolId { get; set; }
        public string? CoverImagePath { get; set; }
    }
}
