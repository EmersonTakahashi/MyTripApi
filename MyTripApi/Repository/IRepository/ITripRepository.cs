using Microsoft.EntityFrameworkCore.ChangeTracking;
using MyTripApi.Models.Dto.Trip;
using MyTripApi.Models.Entities;
using System.Linq.Expressions;

namespace MyTripApi.Repository.IRepository
{
    public interface ITripRepository : IRepositoryBase<Trip>
    {
        new Task<Trip> UpdateAsync(Trip trip);
    }
}
