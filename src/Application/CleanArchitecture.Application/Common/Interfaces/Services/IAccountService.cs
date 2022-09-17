

using CleanArchitecture.Application.Common.Models.Identity;
using CleanArchitecture.Application.Services.Account.Dto;

namespace CleanArchitecture.Application.Common.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AuthResponse> RegisterAsync(RegistrationRequestDto request);
        Task<AuthResponse> AuthenticateAsync(AuthRequestDto request);
        Task<bool> ChangePassword(ChangePasswordDto changePasswordDto);
    }
}
