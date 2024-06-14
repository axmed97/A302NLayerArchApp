using Entities.Common;
using Microsoft.AspNetCore.Http;

namespace Business.Utilities.Storage.Abstract
{
    public interface IStorage
    {
        Task<Upload> UploadFileAsync(string path, IFormFile file);
    }
}
