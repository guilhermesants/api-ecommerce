using Amazon.S3;
using Amazon.S3.Transfer;
using Application.Services.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Application.Services.Concrets;

internal class MinIoStorageService : IStorageService
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucket;

    public MinIoStorageService(IAmazonS3 s3Client, IConfiguration config)
    {
        _s3Client = s3Client;
        _bucket = config["Minio:BucketName"]!;
    }

    public async Task<string> UploadFileAsync(IFormFile file, string fileName, CancellationToken cancellationToken)
    {
        using var newMemoryStream = new MemoryStream();
        await file.CopyToAsync(newMemoryStream, cancellationToken);

        var uploadRequest = new TransferUtilityUploadRequest
        {
            InputStream = newMemoryStream,
            Key = fileName,
            BucketName = _bucket,
            ContentType = file.ContentType,
            CannedACL = S3CannedACL.PublicRead
        };

        var transferUtility = new TransferUtility(_s3Client);
        await transferUtility.UploadAsync(uploadRequest, cancellationToken);

        return $"http://localhost:9000/{_bucket}/{fileName}";
    }
}
