using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Persistence.DatabaseContext;
using CleanArchitecture.Infrastructure.Persistence.DataSeedings;
using CleanArchitecture.WebApi.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.WebApi.Configurations
{
    public static class ConfigureWebApiMiddlewares
    {
        public static async Task UseWebApiMiddlewares(this WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<PerformanceMiddleware>();
            await MigrateAndInitializeDatabase(app);
        }

        private static async Task MigrateAndInitializeDatabase(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    await MigrateDatabaseAsync(services);

                    await InitializeDatabase(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
                }
            }

            static async Task InitializeDatabase(IServiceProvider services)
            {
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                await ApplicationDataSeed.SeedApplicationUsers(userManager, roleManager);
            }

            static async Task MigrateDatabaseAsync(IServiceProvider services)
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                if (context.Database.EnsureCreated())
                {
                    await context.Database.MigrateAsync();
                }
            }
        }
    }
}
