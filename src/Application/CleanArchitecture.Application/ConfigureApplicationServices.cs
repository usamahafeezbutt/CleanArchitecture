

using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces.Services;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Services.Account;
using CleanArchitecture.Application.Services.Account.Dto;
using CleanArchitecture.Application.Services.Account.Dto.Validators;
using CleanArchitecture.Application.Services.Customer;
using FluentValidation;
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
            ConfigureApplicationService(services);
            ConfigureValidators(services);
            return services;
        }

        private static void ConfigureValidators(IServiceCollection services)
        {
            services.AddTransient<IValidator<RegistrationRequestDto>, RegistrationRequestDtoValidator>();
            services.AddTransient<IValidator<AuthRequestDto>, AuthRequestDtoValidator>();
            services.AddTransient<IValidator<ChangePasswordDto>, ChangePasswordDtoValidator>();
        }

        private static void ConfigureApplicationService(IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IAccountService, AccountService>();
        }
    }
}
