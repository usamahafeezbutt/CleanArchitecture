
using CleanArchitecture.Application.Common.Models.Identity;
using CleanArchitecture.Application.Common.Models.Response;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Interfaces.Identity
{
    public interface ITokenService
    {
        public Task<Response<AuthResponse>> GenerateUserToken(ApplicationUser user);
    }
}
