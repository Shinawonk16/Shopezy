using Application.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace Persistence.UploadImage
{
    public class UploadImages:IShopezyImage
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UploadImages(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public string UploadFiles(IFormFile formFile)
        {
            var pathName = Path.Combine(_webHostEnvironment.WebRootPath, "Shopezy/Images");
            var filename = Guid.NewGuid().ToString() + "_" + formFile.FileName;
            if (!Directory.Exists(pathName))
            {
                Directory.CreateDirectory(pathName);
            }
            var absolutePath = Path.Combine(pathName, filename);
            formFile.CopyTo(new FileStream(absolutePath, FileMode.Create));
            return filename;
        }

       
    }
}