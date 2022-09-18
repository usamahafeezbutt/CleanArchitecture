using CleanArchitecture.Application;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.Infrastructure.Persistence.DatabaseContext;
using CleanArchitecture.WebApi.Filters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace CleanArchitecture.WebApi.Configurations
{
    public static class ConfigureWebApiServices
    {
        public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configurations)
        {
            services.AddSwaggerGen(setup =>
            {
                // Include 'SecurityScheme' to use JWT Authentication
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
            });
            services.AddControllers(options =>
            {
                options.Filters.Add<ExceptionFilter>();
            });
            services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            services.AddApplication(configurations);
            services.AddHealthChecks()
                        .AddDbContextCheck<ApplicationDbContext>();
            services.AddInfrastructure(configurations);
            return services;
        }
    }
}
