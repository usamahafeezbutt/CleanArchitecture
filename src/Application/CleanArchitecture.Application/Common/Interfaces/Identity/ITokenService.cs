
using CleanArchitecture.Application.Common.Models.Identity;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Interfaces.Identity
{
    public interface ITokenService
    {
        public AuthResponse GenerateUserToken(ApplicationUser user);
    }
}
