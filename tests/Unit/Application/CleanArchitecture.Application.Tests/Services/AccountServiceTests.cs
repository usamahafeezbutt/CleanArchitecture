

using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces.Identity;
using CleanArchitecture.Application.Common.Models.Identity;
using CleanArchitecture.Application.Common.Models.Response;
using CleanArchitecture.Application.Services.Account;
using CleanArchitecture.Application.Services.Account.Dto;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;
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
        public async Task Should_RegisterAsync_SuccessfullAsync()
        {
            //Arrange
            RegistrationRequestDto registrationRequestDto = new() { Email = "Test@example.com", Password = "12345" };
            ApplicationUser user = new()
            {
                UserName = "Test",
                Email = "Test@example.com"
            };
            Response<AuthResponse> authResponse = new()
            {
                Success = true,
                Message = "",
                Result = new AuthResponse
                {
                    Email = "Test@example.com",
                    Token = "TestToken",
                    Expiry = 1
                }
            };
            _mapper.Setup(mapper => mapper.Map<ApplicationUser>(It.IsAny<RegistrationRequestDto>())).Returns(user);    
            _identityService.Setup(identityService => identityService.CreateUserAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            _identityService.Setup(identityService => identityService.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            _tokenService.Setup(tokenService => tokenService.GenerateUserToken(It.IsAny<ApplicationUser>())).ReturnsAsync(authResponse);
            //Act
            var result = await _accountService.RegisterAsync(registrationRequestDto);
            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.Result!.Email, registrationRequestDto.Email);
        }

        [Fact]
        public async Task Should_Not_RegisterAsync_SuccessfullAsync()
        {
            //Arrange
            RegistrationRequestDto registrationRequestDto = new() { Email = "Test@example.com", Password = "12345" };
            _mapper.Setup(mapper => mapper.Map<ApplicationUser>(It.IsAny<RegistrationRequestDto>())).Throws(new Exception());
            //Act
            var result = async () => await _accountService.RegisterAsync(registrationRequestDto);
            var exception = await Assert.ThrowsAsync<Exception>(result);
            //Assert
            Assert.NotNull(exception);
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
            Response<AuthResponse> authResponse = new()
            {
                Success = true,
                Message = "",
                Result = new AuthResponse
                {
                    Email = "Test@example.com",
                    Token = "TestToken",
                    Expiry = 1
                }
            };
            _identityService.Setup(identityService => identityService.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            _identityService.Setup(identityService => identityService.CheckPasswordAsync(It.IsAny<ApplicationUser>(),It.IsAny<string>())).ReturnsAsync(true);
            _tokenService.Setup(tokenService => tokenService.GenerateUserToken(It.IsAny<ApplicationUser>())).ReturnsAsync(authResponse);
            //Act
            var result = await _accountService.AuthenticateAsync(authRequestDto);
            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.Result!.Email, authRequestDto.Email);
        }

        [Fact]
        public async Task Should_Not_AuthenticateAsync_SuccessfullAsync()
        {
            //Arrange
            AuthRequestDto authRequestDto = new() { Email = "Test@example.com", Password = "12345" };
            _identityService.Setup(identityService => identityService.FindByEmailAsync(It.IsAny<string>())).ThrowsAsync(new Exception());
            //Act
            var result = async () => await _accountService.AuthenticateAsync(authRequestDto);
            var exception = await Assert.ThrowsAsync<Exception>(result);
            //Assert
            Assert.NotNull(exception);
        }

        [Fact]
        public async Task Should_ChangePassword_SuccessfullAsync()
        {
            ChangePasswordDto changePasswordDto = new() { Password = "12345", NewPassword = "12345678", ConfirmPassword = "12345678" };
            ApplicationUser user = new()
            {
                UserName = "Test",
                Email = "Test@example.com"
            };
            Response<AuthResponse> authResponse = new()
            {
                Success = true,
                Message = "",
                Result = new AuthResponse
                {
                    Email = "Test@example.com",
                    Token = "TestToken",
                    Expiry = 1
                }
            };
            _currentUserService.Setup(user => user.UserId).Returns("TestId");
            _identityService.Setup(identityService => identityService.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
            _identityService.Setup(identityService => identityService.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(true);
            _identityService.Setup(identityService => identityService.ChangePasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            //Act
            var result = await _accountService.ChangePassword(changePasswordDto);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Not_ChangePasswordAsync_SuccessfullAsync()
        {
            //Arrange
            ChangePasswordDto changePasswordDto = new() { Password = "12345", NewPassword = "12345678", ConfirmPassword = "12345678" };
            _currentUserService.Setup(user => user.UserId).Returns("TestId");
            _identityService.Setup(identityService => identityService.FindByIdAsync(It.IsAny<string>())).ThrowsAsync(new Exception());
            //Act
            var result = async () => await _accountService.ChangePassword(changePasswordDto);
            var exception = await Assert.ThrowsAsync<Exception>(result);
            //Assert
            Assert.NotNull(exception);
        }
    }
}
