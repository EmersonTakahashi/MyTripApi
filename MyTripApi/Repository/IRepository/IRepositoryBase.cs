using MyTripApi.Models;
using System.Linq.Expressions;

namespace MyTripApi.Repository.IRepository
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
        Task<T?> GetAsync(Expression<Func<T, bool>> filter, bool tracked = true);
        Task CreateAsync(T entity);
        Task RemoveAsync(T entity);
        Task SaveAsync();
        Task UpdateAsync(T entity);
    }
}
