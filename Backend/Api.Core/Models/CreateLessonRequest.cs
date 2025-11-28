using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Models
{
    public class CreateLessonRequest
    {
        public string Title { get; set; } = string.Empty;
        public string? ContentUrl { get; set; }  
        public int? DurationMinutes { get; set; }
    }
}
