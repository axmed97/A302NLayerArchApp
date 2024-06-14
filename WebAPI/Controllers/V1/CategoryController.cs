using Asp.Versioning;
using Business.Abstract;
using Entities.DTOs.CategoryDTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpPost("[action]")]
        [MapToApiVersion("1.0")]
        public IActionResult Create(List<AddCategoryDTO> models)
        {
            _categoryService.Create(models);
            return Ok();
        }


        [HttpPut("[action]/{id}")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> Update(Guid id, List<UpdateCategoryDTO> models)
        {
            await _categoryService.Update(id, models);
            return Ok();
        }

        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        public IActionResult Delete(Guid id)
        {
            _categoryService.Delete(id);
            return Ok();
        }

        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        public IActionResult Get(Guid id)
        {
            string langCode = Request.Headers.AcceptLanguage;
            if (langCode != "az" || langCode != "en-US" || langCode != "ru-RU")
            {
                var res = _categoryService.GetByLang(id, "az");
                return Ok(res);
            }
            var result = _categoryService.GetByLang(id, langCode);
            return Ok(result);
        }

        [HttpGet("GetAll")]
        [MapToApiVersion("1.0")]
        public IActionResult GetAll()
        {
            //string langCode = Request.Headers["accept-language"];
            string langCode = Thread.CurrentThread.CurrentCulture.Name;
            var result = _categoryService.GetAllByLang(langCode);
            return Ok(result);
        }
    }
}
