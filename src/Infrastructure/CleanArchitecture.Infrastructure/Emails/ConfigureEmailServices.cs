using CleanArchitecture.Application.Common.Interfaces.Emails;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.Emails
{
    public static class ConfigureEmailServices
    {
        public static IServiceCollection AddEmailServices(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
            return services;
        }
    }
}
