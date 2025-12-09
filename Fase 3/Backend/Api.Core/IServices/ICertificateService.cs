using System.Threading.Tasks;
using Api.Core.DTO;

namespace Api.Core.IServices
{
    public interface ICertificateService
    {
        // Ya existían
        Task<CertificateResponse> GenerateCertificateAsync(long enrollmentId);
        Task<(byte[] File, string FileName)> DownloadAsync(long certificateId);

        // 🔹 Nuevo: generar directamente el PDF de un certificado de curso
        Task<byte[]> GenerateCourseCertificatePdfAsync(
           long courseId,
           long studentId,
           decimal? finalGradeFromClient = null
       );
    }
}
