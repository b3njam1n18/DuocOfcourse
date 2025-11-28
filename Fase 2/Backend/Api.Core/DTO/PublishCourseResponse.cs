using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.DTO
{
    public class PublishCourseResponse
    {
        public long CourseId { get; set; }
        public bool IsPublished { get; set; }
        public DateTime PublishedAt { get; set; }
        public string Message { get; set; } 
    }
}
