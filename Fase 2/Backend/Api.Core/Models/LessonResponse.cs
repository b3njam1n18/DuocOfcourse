using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Models
{
    public class LessonResponse
    {
        public long Id { get; set; }
        public long ModuleId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? ContentUrl { get; set; }
        public int? DurationMinutes { get; set; }
        public int Position { get; set; }
    }
}
