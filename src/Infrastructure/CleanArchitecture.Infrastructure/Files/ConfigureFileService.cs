using CleanArchitecture.Application.Common.Interfaces.Files;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.Files
{
    public static class ConfigureFileService
    {
        public static IServiceCollection AddFileServices(this IServiceCollection services)
        {
            services.AddScoped<IFileService, FileService>();
            return services;
        }
    }
}
