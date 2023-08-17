using Microsoft.AspNetCore.Http;

namespace Application.Abstractions;

public interface IShopezyImage
{
    string UploadFiles(IFormFile formFile);
}