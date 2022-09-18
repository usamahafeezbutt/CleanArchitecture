using CleanArchitecture.Application.Common.Configurations.Settings.Emails;
using CleanArchitecture.Application.Common.Interfaces.Emails;
using CleanArchitecture.Application.Common.Models.Emails;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.Emails
{
    public static class ConfigureEmailServices
    {
        public static IServiceCollection AddEmailServices(this IServiceCollection services, IConfiguration configurations)
        {
            services.AddScoped<IEmailService, EmailService>();
            services.Configure<SmtpSettings>(configurations.GetSection(nameof(SmtpSettings)));
            return services;
        }
    }
}
