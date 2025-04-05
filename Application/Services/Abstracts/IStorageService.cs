using Microsoft.AspNetCore.Http;

namespace Application.Services.Abstracts;

public interface IStorageService
{
    Task<string> UploadFileAsync(IFormFile file, string fileName, CancellationToken cancellationToken);
}
