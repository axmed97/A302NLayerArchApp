using Asp.Versioning;
using Business.Abstract;
using Entities.DTOs.SubCategoryDTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryService _subCategoryService;

        public SubCategoryController(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }
        [HttpPost]
        public IActionResult Create(AddSubCategoryDTO model)
        {
            var result = _subCategoryService.Create(model);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
