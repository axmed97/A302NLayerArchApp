using Asp.Versioning;
using Business.Utilities.Storage.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IStorageService _storageService;

        public UploadController(IStorageService storageService)
        {
            _storageService = storageService;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(string path, IFormFile file)
        {
            var result = await _storageService.UploadFileAsync(path, file);
            return Ok(result);
        }
    }
}
