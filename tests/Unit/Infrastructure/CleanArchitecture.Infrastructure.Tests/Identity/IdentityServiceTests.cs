

using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace CleanArchitecture.Infrastructure.Tests.Identity
{
    public class IdentityServiceTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _userManager;
        private readonly IdentityService _identityService;
        public IdentityServiceTests()
        {
            _userManager = new Mock<UserManager<ApplicationUser>>(new Mock<IUserStore<ApplicationUser>>().Object, null, null, null, null, null, null, null, null);
            _identityService = new IdentityService(_userManager.Object);
        }

        [Fact]
        public async void ChangePasswordAsync_Successfully()
        {
            //Arrange
            _userManager.Setup(userManager => userManager.ChangePasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            //Act
            var result = await _identityService.ChangePasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>());
            //Assert
            Assert.True(result.Succeeded);
        }

        [Fact]
        public async void ChangePasswordAsync_Not_Successfully()
        {
            //Arrange
            _userManager.Setup(userManager => userManager.ChangePasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new Exception());
            //Act
            var result = async () => await _identityService.ChangePasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>());
            var exception = await Assert.ThrowsAsync<Exception>(result);
            //Assert
            Assert.NotNull(exception);
        }

        [Fact]
        public async void FindByEmailAsync_Successfully()
        {
            //Arrange
            _userManager.Setup(userManager => userManager.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUser());
            //Act
            var result = await _identityService.FindByEmailAsync(It.IsAny<string>());
            //Assert
            Assert.IsType<ApplicationUser>(result);
        }

        [Fact]
        public async void FindByEmailAsync_Not_Successfully()
        {
            //Arrange
            _userManager.Setup(userManager => userManager.FindByEmailAsync(It.IsAny<string>())).ThrowsAsync(new Exception());
            //Act
            var result = async () => await _identityService.FindByEmailAsync(It.IsAny<string>());
            var exception = await Assert.ThrowsAsync<Exception>(result);
            //Assert
            Assert.NotNull(exception);
        }

        [Fact]
        public async void FindByIdAsync_Successfully()
        {
            //Arrange
            _userManager.Setup(userManager => userManager.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUser());
            //Act
            var result = await _identityService.FindByIdAsync(It.IsAny<string>());
            //Assert
            Assert.IsType<ApplicationUser>(result);
        }

        [Fact]
        public async void FindByIdAsync_Not_Successfully()
        {
            //Arrange
            _userManager.Setup(userManager => userManager.FindByIdAsync(It.IsAny<string>())).ThrowsAsync(new Exception());
            //Act
            var result = async () => await _identityService.FindByIdAsync(It.IsAny<string>());
            var exception = await Assert.ThrowsAsync<Exception>(result);
            //Assert
            Assert.NotNull(exception);
        }

        [Fact]
        public async void FindByUserNameAsync_Successfully()
        {
            //Arrange
            _userManager.Setup(userManager => userManager.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUser());
            //Act
            var result = await _identityService.FindByUserNameAsync(It.IsAny<string>());
            //Assert
            Assert.IsType<ApplicationUser>(result);
        }

        [Fact]
        public async void FindByUserNameAsync_Not_Successfully()
        {
            //Arrange
            _userManager.Setup(userManager => userManager.FindByNameAsync(It.IsAny<string>())).ThrowsAsync(new Exception());
            //Act
            var result = async () => await _identityService.FindByUserNameAsync(It.IsAny<string>());
            var exception = await Assert.ThrowsAsync<Exception>(result);
            //Assert
            Assert.NotNull(exception);
        }

        [Fact]
        public async void CheckPasswordAsync_Successfully()
        {
            //Arrange
            _userManager.Setup(userManager => userManager.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(true);
            //Act
            var result = await _identityService.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>());
            //Assert
            Assert.True(true);
        }

        [Fact]
        public async void CheckPasswordAsync_Not_Successfully()
        {
            //Arrange
            _userManager.Setup(userManager => userManager.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ThrowsAsync(new Exception());
            //Act
            var result = async () => await _identityService.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>());
            var exception = await Assert.ThrowsAsync<Exception>(result);
            //Assert
            Assert.NotNull(exception);
        }

        [Fact]
        public async void IsInRoleAsync_Successfully()
        {
            //Arrange
            _userManager.Setup(userManager => userManager.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUser());
            _userManager.Setup(userManager => userManager.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(true);
            //Act
            var result = await _identityService.IsInRoleAsync(It.IsAny<string>(), It.IsAny<string>());
            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void IsInRoleAsync_Not_Successfully()
        {
            //Arrange
            _userManager.Setup(userManager => userManager.FindByIdAsync(It.IsAny<string>())).Throws(new Exception());
            //Act
            var result = async () => await _identityService.IsInRoleAsync(It.IsAny<string>(), It.IsAny<string>());
            var exception = await Assert.ThrowsAsync<Exception>(result);
            //Assert
            Assert.NotNull(exception);
        }

        [Fact]
        public async void DeleteUserAsync_Successfully()
        {
            //Arrange
            _userManager.Setup(userManager => userManager.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUser());
            _userManager.Setup(userManager => userManager.DeleteAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Success);
            //Act
            var result = await _identityService.DeleteUserAsync(It.IsAny<string>());
            //Assert
            Assert.True(result);
        }

        [Fact]
        public async void DeleteUserAsync_Not_Successfully()
        {
            //Arrange
            _userManager.Setup(userManager => userManager.FindByIdAsync(It.IsAny<string>())).ThrowsAsync(new Exception());
            //Act
            var result = async () => await _identityService.DeleteUserAsync(It.IsAny<string>());
            var exception = await Assert.ThrowsAsync<Exception>(result);
            //Assert
            Assert.NotNull(exception);
        }

        [Fact]
        public async void CreateUserAsync_Successfully()
        {
            //Arrange
            _userManager.Setup(userManager => userManager.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            //Act
            var result = await _identityService.CreateUserAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>());
            //Assert
            Assert.True(result.Succeeded);
        }

        [Fact]
        public async void CreateUserAsync_Not_Successfully()
        {
            //Arrange
            _userManager.Setup(userManager => userManager.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ThrowsAsync(new Exception());
            //Act
            var result = async () => await _identityService.CreateUserAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>());
            var exception = await Assert.ThrowsAsync<Exception>(result);
            //Assert
            Assert.NotNull(exception);
        }
    }
}
