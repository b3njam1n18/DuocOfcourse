using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Core.IServices;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Api.Service.Service
{
    public class GcsStorageService : IGcpService
    {
        private readonly StorageClient _storageClient;
        private readonly string _bucketName;


        //referencia al bucket creado en google cloud storage
        public GcsStorageService(IConfiguration config)
        {
            var credentialPath = config["GCP:CredentialsPath"];
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialPath);

            _bucketName = config["GCP:BucketName"];
            _storageClient = StorageClient.Create();
        }

        //subir archivo al bucket creado al google cloud storage
        public async Task<string> UploadFileAsync(IFormFile file, string folder)
        {
            using var stream = file.OpenReadStream();
            var objectName = $"{folder}/{Guid.NewGuid()}_{file.FileName}";

            await _storageClient.UploadObjectAsync(_bucketName, objectName, file.ContentType, stream);

            return $"https://storage.googleapis.com/{_bucketName}/{objectName}";
        }
    }
}
