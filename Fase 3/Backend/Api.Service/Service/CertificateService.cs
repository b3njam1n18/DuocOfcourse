using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Core.DTO;
using Api.Core.IRepositories;
using Api.Core.IServices;
using Api.Core.Models;
using Api.Repository;
using Api.Service.Documents;     // CourseCertificateDocument
using Google;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Api.Service.Service
{
    public class CertificateService : ICertificateService
    {
        //WEB HOST ENVIROMENT para guardar pdf, y descargarlos
        private readonly ICertificateRepository _repository;
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _db;

        public CertificateService(ICertificateRepository repository, IWebHostEnvironment env, AppDbContext db)
        {
            _repository = repository;
            _env = env;
            _db = db;
        }
        public async Task<CertificateResponse> GenerateCertificateAsync(long enrollmentId)
        {
            // 1. Generar archivo (luego lo cambiamos a PDF con QuestPDF)
            var now = DateTime.UtcNow;
            var verificationCode = Guid.NewGuid().ToString("N")[..10].ToUpperInvariant();

            var fileName = $"cert_{enrollmentId}_{now:yyyyMMddHHmmss}.pdf";
            var dir = Path.Combine(_env.WebRootPath, "certificates");
            Directory.CreateDirectory(dir);
            var fullPath = Path.Combine(dir, fileName);

            // Por ahora un archivo de texto de prueba
            File.WriteAllText(fullPath, "PDF DE CERTIFICADO (placeholder)");

            // 2. Guardar en BD
            var cert = new Certificates
            {
                EnrollmentId = enrollmentId,
                PdfPath = fullPath,
                IssuedAt = now,
                VerificationCode = verificationCode,
                GradeSummary = null
            };

            cert = await _repository.CreateAsync(cert);

            return new CertificateResponse
            {
                CertificateId = cert.Id,
                DownloadUrl = $"/api/enrollments/certificate/{cert.Id}/download",
                VerificationCode = cert.VerificationCode,
                IssuedAt = cert.IssuedAt
            };
        }

        public async Task<(byte[] File, string FileName)> DownloadAsync(long certificateId)
        {
            var cert = await _repository.GetByIdAsync(certificateId);
            if (cert == null)
                throw new Exception("Certificado no encontrado.");

            var bytes = await File.ReadAllBytesAsync(cert.PdfPath);
            var name = Path.GetFileName(cert.PdfPath);

            return (bytes, name);
        }
        public async Task<byte[]> GenerateCourseCertificatePdfAsync(
    long courseId,
    long studentId,
    decimal? finalGradeFromClient = null
)
        {
            // 1) Nota final: viene del front (si NO viene, usamos 4.0 como fallback)
            var finalGrade = finalGradeFromClient ?? 4.0m;

            // 2) Traer alumno desde la BD
            var student = await _db.Users
                .FirstOrDefaultAsync(u => u.Id == studentId);

            string studentFullName;

            if (student != null)
            {
                var parts = new[]
                {
            student.FirstName,
            student.MiddleName,
            student.LastName,
            student.SecondLastName
        }
                .Where(p => !string.IsNullOrWhiteSpace(p));

                studentFullName = string.Join(" ", parts);

                if (string.IsNullOrWhiteSpace(studentFullName))
                    studentFullName = $"Alumno {studentId}";
            }
            else
            {
                studentFullName = $"Alumno {studentId}";
            }

            // 3) Traer curso + profesor
            var course = await _db.Courses
                .Include(c => c.Teacher)   // 👈 asumiendo que tienes navegación Teacher
                .FirstOrDefaultAsync(c => c.Id == courseId);

            string courseName;
            string teacherName;

            if (course != null)
            {
                // Usamos solo Title (ajusta si tu propiedad se llama distinto)
                courseName = !string.IsNullOrWhiteSpace(course.Title)
                    ? course.Title
                    : $"Curso {courseId}";

                if (course.Teacher != null)
                {
                    var tParts = new[]
                    {
            course.Teacher.FirstName,
            course.Teacher.MiddleName,
            course.Teacher.LastName,
            course.Teacher.SecondLastName
        }
                    .Where(p => !string.IsNullOrWhiteSpace(p));

                    teacherName = string.Join(" ", tParts);
                    if (string.IsNullOrWhiteSpace(teacherName))
                        teacherName = "Profesor/a";
                }
                else
                {
                    teacherName = "Profesor/a";
                }
            }
            else
            {
                courseName = $"Curso {courseId}";
                teacherName = "Profesor/a";
            }


            // 4) Estado según nota
            string statusText = finalGrade >= 4.0m ? "APROBADO" : "REPROBADO";

            // 5) Armar el modelo para el PDF
            var data = new CourseCertificateData
            {
                StudentFullName = studentFullName,
                CourseName = courseName,
                StatusText = statusText,
                FinalGrade = finalGrade,
                IssuedAt = DateTime.Now, // zona horaria del servidor
                CertificateCode = Guid.NewGuid().ToString("N")[..10].ToUpper(),
                TeacherName = teacherName
            };

            // 6) Generar PDF con QuestPDF
            var document = new CourseCertificateDocument(data);
            byte[] pdfBytes = document.GeneratePdf();

            return pdfBytes;
        }


    }
}
