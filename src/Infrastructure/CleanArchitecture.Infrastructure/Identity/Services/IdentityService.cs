using CleanArchitecture.Application.Common.Interfaces.Identity;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Infrastructure.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public IdentityService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword)
        => await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);


        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        => await _userManager.CheckPasswordAsync(user, password);


        public async Task<IdentityResult> CreateUserAsync(ApplicationUser applicationUser, string password)
        => await _userManager.CreateAsync(applicationUser, password);

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is not null)
            {
                var result = await _userManager.DeleteAsync(user);
                return result.Succeeded;
            }
            return false;
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        => await _userManager.FindByEmailAsync(email);

        public async Task<ApplicationUser> FindByIdAsync(string id)
        => await _userManager.FindByIdAsync(id);

        public async Task<ApplicationUser> FindByUserNameAsync(string userName)
        => await _userManager.FindByNameAsync(userName);

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user is not null && await _userManager.IsInRoleAsync(user, role);
        }
    }
}
