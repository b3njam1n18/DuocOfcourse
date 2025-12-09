// Api.Core/DTO/CourseCertificateData.cs
using System;

namespace Api.Core.DTO
{
    public class CourseCertificateData
    {
        public string StudentFullName { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;
        public decimal FinalGrade { get; set; }
        public string StatusText { get; set; } = string.Empty;
        public DateTime IssuedAt { get; set; }
        public string CertificateCode { get; set; } = string.Empty;
        public string TeacherName { get; set; } = string.Empty;
    }
}
