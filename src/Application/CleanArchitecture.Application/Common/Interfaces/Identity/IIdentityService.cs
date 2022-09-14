using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Application.Common.Interfaces.Identity
{
    public interface IIdentityService
    {
        Task<IdentityResult> CreateUserAsync(ApplicationUser applicationUser, string password);

        Task<ApplicationUser> FindByEmailAsync(string email);

        Task<ApplicationUser> FindByIdAsync(string id);
        
        Task<ApplicationUser> FindByUserNameAsync(string userName);

        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);

        Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword);
        
        Task<bool> IsInRoleAsync(string userId, string role);

        Task<bool> DeleteUserAsync(string userId);
    }
}
