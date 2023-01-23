using Microsoft.EntityFrameworkCore;
using MyTripApi.Data;
using MyTripApi.Repository.IRepository;
using System.Linq.Expressions;

namespace MyTApi.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class 
    {
        private readonly MyTripDbContext _dbContext;
        internal DbSet<T> dbSet;
        public RepositoryBase(MyTripDbContext dbContext)
        {
            _dbContext = dbContext;
            dbSet = _dbContext.Set<T>();
        }
        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            _dbContext.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
    
}
