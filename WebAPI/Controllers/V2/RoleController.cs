using Asp.Versioning;
using Business.Abstract;
using Entities.DTOs.RoleDTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleDTO createRoleDTO)
        {
            var result = await _roleService.CreateRole(createRoleDTO);
            return Ok(result);
        }
    }
}
