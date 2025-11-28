using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Core.DTO;

namespace Api.Core.IServices
{
    public interface ICertificateService
    {
        public Task<CertificateResponse> GenerateCertificateAsync(long enrollmentId);
        public Task<(byte[] File, string FileName)> DownloadAsync(long certificateId);

    }
}
