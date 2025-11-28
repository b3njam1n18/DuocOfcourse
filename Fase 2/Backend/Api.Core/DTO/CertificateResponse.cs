using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.DTO
{
    public class CertificateResponse
    {
        public long CertificateId { get; set; }
        public string DownloadUrl { get; set; }
        public string VerificationCode { get; set; }
        public DateTime IssuedAt { get; set; }
    }
}
