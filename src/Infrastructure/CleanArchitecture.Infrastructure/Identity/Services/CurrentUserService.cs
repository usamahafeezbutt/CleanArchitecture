using CleanArchitecture.Application.Common.Interfaces.Identity;
using Microsoft.AspNetCore.Http;
using System.Data.Common;
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

        public string UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)!;
    }
}
