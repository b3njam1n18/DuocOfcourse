using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Models
{
    public class Media
    {
        public long Id { get; set; }
        public long CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string MimeType { get; set; } = string.Empty;
        public string StoragePath { get; set; } = string.Empty;
        public long? SizeBytes { get; set; }
        public string? ChecksumSha256 { get; set; }
        public DateTime UploadedAt { get; set; }
        public long UploadedBy { get; set; }

        public Users? Uploader { get; set; }
        public Courses? Course { get; set; }
    }
}
