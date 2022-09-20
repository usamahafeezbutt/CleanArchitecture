

using CleanArchitecture.Application.Common.Models.Identity;
using CleanArchitecture.Application.Common.Models.Response;
using CleanArchitecture.Application.Services.Account.Dto;

namespace CleanArchitecture.Application.Common.Interfaces.Services
{
    public interface IAccountService
    {
        Task<Response<AuthResponse>> RegisterAsync(RegistrationRequestDto request);
        Task<Response<AuthResponse>> AuthenticateAsync(AuthRequestDto request);
        Task<BaseResponse> ChangePassword(ChangePasswordDto changePasswordDto);
    }
}
