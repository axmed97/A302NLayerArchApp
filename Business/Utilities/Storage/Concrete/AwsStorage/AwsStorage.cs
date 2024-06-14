using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Business.Utilities.Storage.Abstract.AwsStorage;
using Entities.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Business.Utilities.Storage.Concrete.AwsStorage
{
    public class AwsStorage : Storage, IAwsStorage
    {
        private readonly IConfiguration _configuration;
        private readonly AmazonS3Client _amazonS3Client;

        public AwsStorage(IConfiguration configuration)
        {
            _configuration = configuration;
            _amazonS3Client = new AmazonS3Client(_configuration["Aws:AccessKey"], _configuration["Aws:SecretKey"], RegionEndpoint.EUNorth1);
        }

        public async Task<Upload> UploadFileAsync(string path, IFormFile file)
        {
            Upload upload = new();
            string key = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            using var fileStream = file.OpenReadStream();
            var fileTransferUtility = new TransferUtility(_amazonS3Client);
            await fileTransferUtility.UploadAsync(fileStream, path, key);

            upload.FileName = key;
            upload.Path = $"https://{path}.s3.eu-north-1.amazonaws.com/";

            return upload;
        }
    }
}
