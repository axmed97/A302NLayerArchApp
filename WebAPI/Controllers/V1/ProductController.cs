using Asp.Versioning;
using Business.Abstract;
using Entities.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [MapToApiVersion("1.0")]
        [HttpPost]
        public async Task<IActionResult> Create(AddProductDTO model)
        {
            var result = await _productService.CreateAsync(model);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> Update(Guid id, UpdateProductDTO model)
        {
            var result = await _productService.UpdateAsync(id, model);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        public IActionResult Get(Guid id)
        {
            var lang = Thread.CurrentThread.CurrentCulture.Name;
            var result = _productService.GetById(id, lang);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
