

using CleanArchitecture.Infrastructure.Emails;
using CleanArchitecture.Infrastructure.Files;
using CleanArchitecture.Infrastructure.Identity;
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure
{
    public static class ConfigureInfrastructure
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configurations)
        {
            services.AddPersistence(configurations);
            services.AddIdentityServices(configurations);
            services.AddFileServices();
            services.AddEmailServices(configurations);
            return services;
        }
    }
}
