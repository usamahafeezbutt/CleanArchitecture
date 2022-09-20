

using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Identity.Models;
using CleanArchitecture.Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;

namespace CleanArchitecture.Infrastructure.Tests.Identity
{
    public class TokenServiceTests
    {
        private readonly TokenService _tokenService;
        private readonly IOptions<JwtSettings> _jwtSettings;
        private readonly Mock<UserManager<ApplicationUser>> _userManager;

        public TokenServiceTests()
        {
            _userManager = new Mock<UserManager<ApplicationUser>>(new Mock<IUserStore<ApplicationUser>>().Object, null, null, null, null, null, null, null, null);
            _jwtSettings = Options.Create(new JwtSettings { Secret = "111111111111111111111111111111111111111111111111111", Expiry = 1 });
            _tokenService = new TokenService(_jwtSettings, _userManager.Object);
        }

        [Fact]
        public async void GenerateUserToken_Successfully()
        {
            //Arrange
            var user = new ApplicationUser
            {
                Email = "test@example.com",
                UserName = "test",
                Id = "testid"
            };
            _userManager.Setup(userManager => userManager.GetRolesAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(new List<string> { "test" });
            //Act
            var result = await _tokenService.GenerateUserToken(user);
            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.Result!.Email, user.Email);
        }

        [Fact]
        public async void GenerateUserToken_Not_Successfully()
        {
            //Arrange
            var user = new ApplicationUser
            {
                Email = "test@example.com",
                UserName = "test",
                Id = "testid"
            };
            _userManager.Setup(userManager => userManager.GetRolesAsync(It.IsAny<ApplicationUser>())).ThrowsAsync(new Exception());
            //Act
            var result = async () => await _tokenService.GenerateUserToken(user);
            var exception = await Assert.ThrowsAsync<Exception>(result);
            //Assert
            Assert.NotNull(result);
        }
    }
}
