using Core.Utilities.Results.Abstract;
using Entities.DTOs.RoleDTOs;

namespace Business.Abstract
{
    public interface IRoleService
    {
        Task<IResult> CreateRole(CreateRoleDTO model);
    }
}
