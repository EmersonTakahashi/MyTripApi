using Microsoft.EntityFrameworkCore;
using MyTApi.Repository;
using MyTripApi.Data;
using MyTripApi.Models.Entities;
using MyTripApi.Repository.IRepository;
using System.Linq.Expressions;

namespace MyTripApi.Repository
{
    public class ToDoBeforeTripRepository : RepositoryBase<ToDoBeforeTrip>, IToDoBeforeTripRepository
    {
        private readonly MyTripDbContext _dbContext;
        public ToDoBeforeTripRepository(MyTripDbContext dbContext) :base(dbContext)
        {
            _dbContext = dbContext;
        }
    
        public async Task<ToDoBeforeTrip> UpdateAsync(ToDoBeforeTrip toDoBeforeTrip)
        {
            toDoBeforeTrip.UpdatedAt = DateTime.UtcNow;
            _dbContext.ToDoBeforeTrip.Update(toDoBeforeTrip);
            await _dbContext.SaveChangesAsync();
            return toDoBeforeTrip;
        }
    }
}
