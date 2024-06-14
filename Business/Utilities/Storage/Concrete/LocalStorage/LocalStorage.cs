using Business.Utilities.Storage.Abstract.LocalStorage;
using Entities.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Business.Utilities.Storage.Concrete.LocalStorage
{
    public class LocalStorage : Storage, ILocalStorage
    {
        private readonly IWebHostEnvironment _env;

        public LocalStorage(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<Upload> UploadFileAsync(string path, IFormFile file)
        {
            string uploadPath = Path.Combine(_env.WebRootPath, path);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var newFileName = Guid.NewGuid() + file.FileName;
            var pathCombine = Path.Combine(uploadPath, newFileName);

            using FileStream fileStream = new(pathCombine, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return new Upload
            {
                FileName = newFileName,
                Path = uploadPath
            };
        }
    }
}
