using Microsoft.EntityFrameworkCore.ChangeTracking;
using MyTripApi.Models;
using MyTripApi.Models.Dto.Trip;
using System.Linq.Expressions;

namespace MyTripApi.Repository.IRepository
{
    public interface ITripRepository : IRepositoryBase<Trip>
    {
        new Task<Trip> UpdateAsync(Trip trip);
    }
}
