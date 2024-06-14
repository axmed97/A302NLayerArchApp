using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Entities.DTOs.AuthDTOs;

namespace Business.Abstract
{
    public interface IAuthService
    {
        Task<IResult> RegisterAsync(RegisterDTO model);
        Task<IDataResult<Token>> LoginAsync(LoginDTO loginDTO);
        Task<IDataResult<string>> UpdateRefreshToken(string refreshToken, AppUser appUser);
        Task<IDataResult<Token>> RefreshTokenLoginAsync(string refreshToken);
        Task<IResult> Logout(string userId);
        Task<IResult> AssignRoleToUserAsync(string userId, string role);
        Task<IResult> UserEmailConfirmed(string email, string otp);
    }
}
