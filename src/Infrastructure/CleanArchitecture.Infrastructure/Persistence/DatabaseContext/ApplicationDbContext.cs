

using CleanArchitecture.Application.Common.Interfaces.DatabaseContext;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Persistence.DatabaseContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int> ,IdentityUserToken<int>>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        Task<bool> IApplicationDbContext.SaveChangesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
