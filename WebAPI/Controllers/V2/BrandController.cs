using Asp.Versioning;
using Business.Abstract;
using Entities.DTOs.BrandDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebAPI.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        public readonly IBrandService brandService;

        public BrandController(IBrandService brandService)
        {
            this.brandService = brandService;
        }

        [HttpPost]
        [MapToApiVersion("2.0")]
        public IActionResult Create(AddBrandDTO model)
        {
            var result = brandService.Create(model);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("{id}")]
        [MapToApiVersion("2.0")]
        public IActionResult Get(Guid id)
        {
            var result = brandService.Get(id);
            if (result.StatusCode == HttpStatusCode.NotFound)
                return NotFound(result);
            else if (result.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);
            else
                return Ok(result);
        }

        [HttpPut("{id}")]
        [MapToApiVersion("2.0")]
        public IActionResult Update(Guid id, UpdateBrandDTO model)
        {
            model.Id = id;
            var result = brandService.Update(id, model);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPatch("{id}")]
        [MapToApiVersion("2.0")]
        public IActionResult SoftDelete(Guid id)
        {
            var result = brandService.SoftDelete(id);
            if (result.StatusCode == HttpStatusCode.NotFound)
                return NotFound(result);
            else if (result.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);
            else
                return Ok(result);
        }


    }
}
