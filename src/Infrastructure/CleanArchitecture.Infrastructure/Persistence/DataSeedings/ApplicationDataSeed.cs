

using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Infrastructure.Persistence.DataSeedings
{
    public static class ApplicationDataSeed
    {
        public static async Task SeedApplicationUsers(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var adminRole = new IdentityRole("Administrator");

            if (roleManager.Roles.All(r => r.Name != adminRole.Name))
            {
                await roleManager.CreateAsync(adminRole);
            }
            ApplicationUser admin = InitializeAdmin();
            if (userManager.Users.All(user => user.UserName != admin.UserName))
            {
                await userManager.CreateAsync(admin, "12345678");
                await userManager.AddToRolesAsync(admin, new[] { adminRole.Name });
            }
        }

        private static ApplicationUser InitializeAdmin()
        => new()
        {
            Name = "Usama Hafeez Butt",
            Email = "usamahafeezbutt@gmail.com",
            UserName = "usamahafeezbutt@gmail.com",
            PhoneNumber = "03497876156"
        };
    }
}
