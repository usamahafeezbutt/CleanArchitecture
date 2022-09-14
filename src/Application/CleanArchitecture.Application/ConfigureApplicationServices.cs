

using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application
{
    public static class ConfigureApplicationServices
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configurations)
        {
            var customerMappingProfile = new CustomerMappingsProfile();
            var mappings = new MapperConfiguration(config =>
            {
                config.AddProfile(customerMappingProfile);
            }).CreateMapper();
            services.AddSingleton(mappings);
            return services;
        }
    }
}
