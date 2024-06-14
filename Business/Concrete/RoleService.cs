using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.SuccessResults;
using Entities.DTOs.RoleDTOs;
using Microsoft.AspNetCore.Identity;

namespace Business.Concrete
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IResult> CreateRole(CreateRoleDTO model)
        {
            await _roleManager.CreateAsync(new()
            {
                Name = model.RoleName
            });
            return new SuccessResult(System.Net.HttpStatusCode.Created);
        }
    }
}
