

using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces.Identity;
using CleanArchitecture.Application.Common.Models.Identity;
using CleanArchitecture.Application.Services.Account;
using CleanArchitecture.Application.Services.Account.Dto;
using CleanArchitecture.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;

namespace CleanArchitecture.Application.Tests.Services
{
    public class AccountServiceTests
    {
        private readonly Mock<IIdentityService> _identityService;
        private readonly Mock<ITokenService> _tokenService;
        private readonly Mock<ICurrentUserService> _currentUserService;
        private readonly Mock<ILogger<AccountService>> _logger;
        private readonly Mock<IMapper> _mapper;
        private readonly AccountService _accountService;
        public AccountServiceTests()
        {
            _identityService = new Mock<IIdentityService>();
            _tokenService = new Mock<ITokenService>();
            _currentUserService = new Mock<ICurrentUserService>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger<AccountService>>();
            _accountService = new AccountService(_identityService.Object, _tokenService.Object, _currentUserService.Object, _logger.Object, _mapper.Object);
        }

        [Fact]
        public void Should_RegisterAsync_Successfull()
        {
            //Arrange
            //Act
            //Assert
        }

        [Fact]
        public void Should_Not_RegisterAsync_Successfull()
        {
            //Arrange
            //Act
            //Assert
        }

        [Fact]
        public async Task Should_AuthenticateAsync_Successfull()
        {
            //Arrange
            AuthRequestDto authRequestDto = new() { Email = "Test@example.com", Password = "12345" };
            ApplicationUser user = new()
            {
                UserName = "Test",
                Email = "Test@example.com"
            };
            AuthResponse authResponse = new()
            {
                Email = "Test@example.com",
                Token = "TestToken",
                Expiry = 1
            };
            _identityService.Setup(identityService => identityService.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            _identityService.Setup(identityService => identityService.CheckPasswordAsync(It.IsAny<ApplicationUser>(),It.IsAny<string>())).ReturnsAsync(true);
            _tokenService.Setup(tokenService => tokenService.GenerateUserToken(It.IsAny<ApplicationUser>())).Returns(authResponse);
            //Act
            var result = await _accountService.AuthenticateAsync(authRequestDto);
            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.Email, authRequestDto.Email);
        }

        [Fact]
        public async Task Should_Not_AuthenticateAsync_SuccessfullAsync()
        {
            AuthRequestDto authRequestDto = new() { Email = "Test@example.com", Password = "12345" };
            ApplicationUser user = new()
            {
                UserName = "Test",
                Email = "Test@example.com"
            };
            AuthResponse authResponse = new()
            {
                Email = "Test@example.com",
                Token = "TestToken",
                Expiry = 1
            };
            _identityService.Setup(identityService => identityService.FindByEmailAsync(It.IsAny<string>())).ThrowsAsync(new Exception());
            //Act
            var result = async () => await _accountService.AuthenticateAsync(authRequestDto);
            var exception = await Assert.ThrowsAsync<Exception>(result);
            //Assert
            Assert.NotNull(exception);
        }

        [Fact]
        public void Should_ChangePassword_Successfull()
        {
            //Arrange
            //Act
            //Assert
        }

        [Fact]
        public void Should_Not_ChangePasswordAsync_Successfull()
        {
            //Arrange
            //Act
            //Assert
        }
    }
}
