
using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Common.Interfaces.DatabaseContext
{
    public interface IApplicationDbContext
    {
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken = new());

    }
}
