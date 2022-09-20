using CleanArchitecture.Application.Common.Interfaces.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CleanArchitecture.Infrastructure.Identity.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        private string Identifier => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)!;
        public string UserId => !string.IsNullOrWhiteSpace(Identifier) ? Identifier : "";
    }
}
