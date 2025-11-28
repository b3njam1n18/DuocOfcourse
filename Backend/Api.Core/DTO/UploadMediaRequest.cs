using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.DTO
{
    public class UploadMediaRequest
    {
        public string Title { get; set; } = string.Empty;
        public string MimeType { get; set; } = string.Empty;
        public string StoragePath { get; set; } = string.Empty; // ubicación del archivo
        public long? SizeBytes { get; set; }
        public long UploadedBy { get; set; }
    }
}
