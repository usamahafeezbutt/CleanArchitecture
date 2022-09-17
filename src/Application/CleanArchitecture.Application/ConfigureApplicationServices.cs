

using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces.Services;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Services.Account;
using CleanArchitecture.Application.Services.Customer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application
{
    public static class ConfigureApplicationServices
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configurations)
        {
            var customerMappingProfile = new CustomerMappingsProfile();
            var authMappingProfile = new AuthMappingsProfile();
            var mappings = new MapperConfiguration(config =>
            {
                config.AddProfile(customerMappingProfile);
                config.AddProfile(authMappingProfile);
            }).CreateMapper();
            services.AddSingleton(mappings);
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IAccountService, AccountService>();
            return services;
        }
    }
}
