

using CleanArchitecture.Application.Common.Constants;
using CleanArchitecture.Infrastructure.Persistence.DatabaseContext;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

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
            
            return services;
        }
    }
}
