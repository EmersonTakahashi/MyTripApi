using Microsoft.EntityFrameworkCore;
using MyTApi.Repository;
using MyTripApi.Data;
using MyTripApi.Models.Entities;
using MyTripApi.Repository.IRepository;
using System.Linq.Expressions;

namespace MyTripApi.Repository
{
    public class TripRepository : RepositoryBase<Trip>, ITripRepository
    {
        private readonly MyTripDbContext _dbContext;
        public TripRepository(MyTripDbContext dbContext) :base(dbContext)
        {
            _dbContext = dbContext;
        }
    
        public async Task<Trip> UpdateAsync(Trip trip)
        {
            trip.UpdatedAt = DateTime.UtcNow;
            _dbContext.Trip.Update(trip);
            await _dbContext.SaveChangesAsync();
            return trip;
        }
    }
}
