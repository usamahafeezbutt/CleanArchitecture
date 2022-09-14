

using CleanArchitecture.Application.Common.Interfaces.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.Identity
{
    public static class ConfigureIdentityService
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddScoped<IIdentityService, IdentityService>();
            return services;
        }
    }
}
