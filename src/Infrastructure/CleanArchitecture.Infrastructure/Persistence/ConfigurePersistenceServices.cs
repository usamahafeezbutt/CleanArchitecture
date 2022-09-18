

using CleanArchitecture.Application.Common.Constants;
using CleanArchitecture.Infrastructure.Persistence.DatabaseContext;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Infrastructure.Persistence
{
    public static class ConfigurePersistenceServices
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configurations)
        {
            if (configurations.GetValue<bool>(DbContextConstants.UseInMemoryDatabase))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase(DbContextConstants.CleanArchitectureDb));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                                options.UseSqlServer(configurations.GetConnectionString(DbContextConstants.DefaultConnection)));
            }
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            });

            services.AddIdentity<ApplicationUser,IdentityRole>(config =>
            {
                config.Password.RequiredLength = 6;
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            return services;
        }
    }
}
