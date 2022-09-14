
using CleanArchitecture.Application.Common.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public GenericRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public DbSet<T> Table => _applicationDbContext.Set<T>();

        public IQueryable<T> TableNoTracking => Table.AsNoTracking();

        public async Task<bool> Add(T entity)
        {
            await Table.AddAsync(entity);
            return await _applicationDbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> Add(IList<T> entities)
        {
            await Table.AddRangeAsync(entities);
            return await _applicationDbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(T entity)
        {
            Table.Remove(entity);
            return await _applicationDbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(IList<T> entities)
        {
            Table.RemoveRange(entities);
            return await _applicationDbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExecuteNonQuery(string sqlQuery)
        {
            return await _applicationDbContext.Database.ExecuteSqlRawAsync(sqlQuery) == -1;
        }

        public async Task<bool> Update(T entity)
        {
            _applicationDbContext.Entry(entity).State = EntityState.Modified;
            return await _applicationDbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(IList<T> entities)
        {
            foreach (var entity in entities)
            {
                _applicationDbContext.Entry(entity).State = EntityState.Modified;
            }
            return await _applicationDbContext.SaveChangesAsync() > 0;
        }
    }
}
