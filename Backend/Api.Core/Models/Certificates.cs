using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Models
{
    public class Certificates
    {
        public long Id { get; set; }
        public long EnrollmentId { get; set; }
        public string PdfPath { get; set; } = string.Empty;
        public DateTime IssuedAt { get; set; }
        public string VerificationCode { get; set; } = string.Empty;
        public string? GradeSummary { get; set; }

        public Enrollments Enrollment { get; set; }
    }
}
