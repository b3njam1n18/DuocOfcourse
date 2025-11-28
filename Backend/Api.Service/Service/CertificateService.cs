using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Core.DTO;
using Api.Core.IRepositories;
using Api.Core.IServices;
using Api.Core.Models;
using Microsoft.AspNetCore.Hosting;

namespace Api.Service.Service
{
    public class CertificateService : ICertificateService
    {
        //WEB HOST ENVIROMENT para guardar pdf, y descargarlos
        private readonly ICertificateRepository _repository;
        private readonly IWebHostEnvironment _env;

        public CertificateService(ICertificateRepository repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _env = env;

        }
        public async Task<CertificateResponse> GenerateCertificateAsync(long enrollmentId)
        { 

            // 1. Generar PDF
            string fileName = $"cert_{enrollmentId}_{Guid.NewGuid()}.pdf";
            string fullPath = Path.Combine(_env.WebRootPath, "certificates", fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);

            //esta sentencia genera pdf
            File.WriteAllText(fullPath, "PDF DE CERTIFICADO");

            // 2. Guardar en DB
            var cert = new Certificates
            {
                EnrollmentId = enrollmentId,
                PdfPath = fullPath,
                VerificationCode = Guid.NewGuid().ToString("N")[..10].ToUpper(),
                IssuedAt = DateTime.UtcNow
            };

            await _repository.CreateAsync(cert);

            return new CertificateResponse
            {
                CertificateId = cert.Id,
                DownloadUrl = $"/api/certificates/{cert.Id}/download",
                VerificationCode = cert.VerificationCode,
                IssuedAt = cert.IssuedAt
            };
        }
        //descargar pdf
        public async Task<(byte[] File, string FileName)> DownloadAsync(long certificateId)
        {
            var cert = await _repository.GetByIdAsync(certificateId)
                ?? throw new Exception("Certificado no encontrado");

            byte[] file = File.ReadAllBytes(cert.PdfPath);
            string fileName = $"certificado_{certificateId}.pdf";

            return (file, fileName);
        }
    }
}
