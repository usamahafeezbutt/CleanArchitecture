

using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces.Identity;
using CleanArchitecture.Application.Common.Interfaces.Services;
using CleanArchitecture.Application.Common.Models.Identity;
using CleanArchitecture.Application.Services.Account.Dto;
using CleanArchitecture.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IIdentityService _identityService;
        private readonly ITokenService _tokenService;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<AccountService> _logger;
        private readonly IMapper _mapper;
        public AccountService(IIdentityService identityService, ITokenService tokenService, ICurrentUserService currentUserService, ILogger<AccountService> logger, IMapper mapper)
        {
            _identityService = identityService;
            _tokenService = tokenService;
            _currentUserService = currentUserService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<AuthResponse> AuthenticateAsync(AuthRequestDto request)
        {
            var user = await _identityService.FindByEmailAsync(request.Email);
            if (user == null)
            {
                _logger.LogError("No user exist with current email address");
                return null!;
            }

            var passwordCheck = await _identityService.CheckPasswordAsync(user, request.Password);
            if (!passwordCheck)
                return null!;
            return  _tokenService.GenerateUserToken(user);
        }

        public async Task<bool> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var user = await _identityService.FindByIdAsync(_currentUserService.UserId.ToString());
            if (user == null)
            {
                _logger.LogError("No user found");
                return false;
            }
            if (!string.Equals(changePasswordDto.NewPassword, changePasswordDto.ConfirmPassword))
                return false;

            var result = await _identityService.ChangePasswordAsync(user, changePasswordDto.Password, changePasswordDto.NewPassword);

            if (!result.Succeeded)
            {
                _logger.LogError($"{_currentUserService.UserId} tried to change the passwort but failed");
                return false!;
            }

            return true;
        }

        public async Task<AuthResponse> RegisterAsync(RegistrationRequestDto request)
        {
            var user = _mapper.Map<ApplicationUser>(request);
            var result = await _identityService.CreateUserAsync(user, request.Password);
            if (!result.Succeeded)
            {
                _logger.LogError("User not created successfully");
                return null!;
            }
            var identityUser = await _identityService.FindByEmailAsync(request.Email);
            return _tokenService.GenerateUserToken(identityUser);
        }
    }
}
