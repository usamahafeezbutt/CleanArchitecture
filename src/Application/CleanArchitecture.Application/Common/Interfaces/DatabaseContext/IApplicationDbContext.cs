

namespace CleanArchitecture.Application.Common.Interfaces.DatabaseContext
{
    public interface IApplicationDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());

    }
}
