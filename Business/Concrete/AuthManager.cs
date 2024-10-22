using Business.Abstract;
using Business.Messages;
using Business.Validations.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Message.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;
using Core.Utilities.Security.Abstract;
using Entities.Common;
using Entities.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Identity;
using Serilog;
using System.Net;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMessageService _messageService;
        public AuthManager(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMessageService messageService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _messageService = messageService;
        }

        public async Task<IResult> AssignRoleToUserAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.AddToRoleAsync(user, role);
            return new SuccessResult(HttpStatusCode.OK);
        }

        public async Task<IDataResult<Token>> LoginAsync(LoginDTO loginDTO)
        {
            var findUser = await _userManager.FindByEmailAsync(loginDTO.UsernameOrEmail);
            if (findUser == null)
                findUser = await _userManager.FindByNameAsync(loginDTO.UsernameOrEmail);


            if (findUser == null)
                return new ErrorDataResult<Token>(message: "User does not exists!", HttpStatusCode.NotFound);

            if (findUser.EmailConfirmed == false)
                return new ErrorDataResult<Token>(message: "User not confirmed", HttpStatusCode.BadRequest);

            var result = await _signInManager.CheckPasswordSignInAsync(findUser, loginDTO.Password, false);
            var userRoles = await _userManager.GetRolesAsync(findUser);

            if (result.Succeeded)
            {
                Token token = await _tokenService.CreateAccessToken(findUser, userRoles.ToList());
                var response = await UpdateRefreshToken(token.RefreshToken, findUser);
                return new SuccessDataResult<Token>(data: token, statusCode: HttpStatusCode.OK, message: response.Message);
            }
            else
            {
                Log.Error("Username or password is not correct");
                return new ErrorDataResult<Token>("Username or password is not correct", HttpStatusCode.BadRequest);
            }

        }

        public async Task<IResult> Logout(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is not null)
            {
                user.RefreshToken = null;
                user.RefreshTokenExpiredDate = null;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return new SuccessResult(statusCode: HttpStatusCode.OK);
                }
                else
                {
                    string response = string.Empty;
                    foreach (var error in result.Errors)
                    {
                        response += error.Description + ". ";
                    };
                    Log.Error(response);
                    return new ErrorResult(response, HttpStatusCode.BadRequest);
                }
            }
            Log.Error(AuthMessage.UserNotFound);
            return new ErrorResult(message: AuthMessage.UserNotFound, HttpStatusCode.Unauthorized);
        }

        public async Task<IDataResult<Token>> RefreshTokenLoginAsync(string refreshToken)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.RefreshToken == refreshToken);
            var userRoles = await _userManager.GetRolesAsync(user);
            if (user != null && user.RefreshTokenExpiredDate > DateTime.Now)
            {
                Token token = await _tokenService.CreateAccessToken(user, userRoles.ToList());
                token.RefreshToken = refreshToken;
                return new SuccessDataResult<Token>(data: token, statusCode: HttpStatusCode.OK);
            }
            else
            {
                return new ErrorDataResult<Token>(statusCode: HttpStatusCode.BadRequest, message: "Yeniden daxil olun");
            }
        }

        private string GenerateOtp()
        {
            byte[] data = new byte[4];

            using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
            rng.GetBytes(data);
            int value = BitConverter.ToInt32(data, 0);
            return Math.Abs(value % 900000).ToString("D6");
        }

        [ValidationAspect(typeof(RegisterValidation))]
        public async Task<IResult> RegisterAsync(RegisterDTO model)
        {
            User newUser = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Username,
                OTP = GenerateOtp(),
                ExpiredDate = DateTime.Now.AddMinutes(3),
                EmailConfirmed = false
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);
            if (result.Succeeded)
            {
                await _messageService.SendMessage(newUser.Email, "Accept", newUser.OTP);
                return new SuccessResult(System.Net.HttpStatusCode.Created);
            }
            else
            {
                string response = string.Empty;
                foreach (var error in result.Errors)
                {
                    response += error.Description + ". ";
                }
                return new ErrorResult(response, System.Net.HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<string>> UpdateRefreshToken(string refreshToken, AppUser appUser)
        {
            if (appUser is not null)
            {
                appUser.RefreshToken = refreshToken;
                appUser.RefreshTokenExpiredDate = DateTime.Now.AddMonths(1);
                var result = await _userManager.UpdateAsync(appUser);
                if (result.Succeeded)
                {
                    return new SuccessDataResult<string>(data: refreshToken, HttpStatusCode.OK);
                }
                else
                {
                    string response = string.Empty;
                    foreach (var error in result.Errors)
                    {
                        response += error.Description + ". ";
                    }
                    return new ErrorDataResult<string>(message: response, HttpStatusCode.BadRequest);
                }
            }
            else
            {
                return new ErrorDataResult<string>(HttpStatusCode.NotFound);
            }
        }

        public async Task<IResult> UserEmailConfirmed(string email, string otp)
        {
            var findUser = _userManager.Users.OfType<User>().FirstOrDefault(x => x.Email == email);

            if (findUser.OTP == otp && findUser.ExpiredDate > DateTime.Now)
            {
                findUser.EmailConfirmed = true;
                await _userManager.UpdateAsync(findUser);
                return new SuccessResult(HttpStatusCode.OK);
            }

            return new ErrorResult(HttpStatusCode.BadRequest);
        }
    }
}
