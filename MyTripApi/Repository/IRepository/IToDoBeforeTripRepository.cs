using Microsoft.EntityFrameworkCore.ChangeTracking;
using MyTripApi.Models.Dto.Trip;
using MyTripApi.Models.Entities;
using System.Linq.Expressions;

namespace MyTripApi.Repository.IRepository
{
    public interface IToDoBeforeTripRepository : IRepositoryBase<ToDoBeforeTrip>
    {
        new Task<ToDoBeforeTrip> UpdateAsync(ToDoBeforeTrip trip);
    }
}
