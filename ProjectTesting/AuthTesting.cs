using Business.Concrete;
using Core.Entities.Concrete;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Security.Abstract;
using Entities.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Net;

namespace ProjectTesting
{
    public class AuthTesting
    {
        private Mock<UserManager<AppUser>> _userManager;
        private Mock<SignInManager<AppUser>> _signInManager;
        private Mock<ITokenService> _tokenService;
        private AuthManager _authManager;

        [SetUp]
        public void Setup()
        {
            // Create the necessary mocks for UserManager dependencies
            var userStoreMock = new Mock<IUserStore<AppUser>>();
            _userManager = new Mock<UserManager<AppUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

            // Create the necessary mocks for SignInManager dependencies
            var contextAccessorMock = new Mock<IHttpContextAccessor>();
            var userClaimsPrincipalFactoryMock = new Mock<IUserClaimsPrincipalFactory<AppUser>>();
            _signInManager = new Mock<SignInManager<AppUser>>(
                _userManager.Object,
                contextAccessorMock.Object,
                userClaimsPrincipalFactoryMock.Object,
                null,
                null,
                null,
                null);

            _tokenService = new Mock<ITokenService>();
            _authManager = new AuthManager(_userManager.Object, _signInManager.Object, _tokenService.Object);
        }

        [Test]
        public async Task LoginAsync_UserNotFound_ReturnsErrorDataResult()
        {
            // Arrange
            var loginDTO = new LoginDTO { UsernameOrEmail = "test@gmail.com", Password = "Test123_" };
            _userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((AppUser)null);
            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((AppUser)null);

            // Act
            var result = await _authManager.LoginAsync(loginDTO);

            // Assert
            Assert.IsInstanceOf<ErrorDataResult<Token>>(result);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
            Assert.AreEqual("User does not exists!", result.Message);
        }

        [Test]
        public async Task LoginAsync_InvalidPassword_ReturnsErrorDataResult()
        {
            // Arrange
            var userName = new AppUser { UserName = "elgulhesenova" };
            var loginDTO = new LoginDTO { UsernameOrEmail = "elgul@gmail.com", Password = "Elgul123_" };
            _userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(userName);
            _signInManager.Setup(x => x.CheckPasswordSignInAsync(userName, loginDTO.Password, false)).ReturnsAsync(SignInResult.Failed);
            // Act
            var result = await _authManager.LoginAsync(loginDTO);

            // Assert
            Assert.IsInstanceOf<ErrorDataResult<Token>>(result);
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.AreEqual("Username or password is not correct", result.Message);
        }

    }
}
