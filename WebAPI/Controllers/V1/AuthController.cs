using Asp.Versioning;
using Business.Abstract;
using Entities.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("[action]")]
        //[Authorize(Roles = "Admin")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            var result = await _authService.RegisterAsync(model);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var result = await _authService.LoginAsync(model);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("refreshToken")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> RefreshTokenLogin([FromBody] RefreshTokenDTO refreshTokenDTO)
        {
            var result = await _authService.RefreshTokenLoginAsync(refreshTokenDTO.RefreshToken);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPut]
        [Authorize]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> Logout()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = await _authService.Logout(userId);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("assign/{userId}")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> AssignRoletoUser([FromRoute] string userId, [FromBody] string role)
        {
            var result = await _authService.AssignRoleToUserAsync(userId, role);
            return Ok(result);
        }


        [HttpPut("[action]")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> EmailConfirmed(string email, string otp)
        {
            var result = await _authService.UserEmailConfirmed(email, otp);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

    }
}
