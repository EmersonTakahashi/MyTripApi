using Microsoft.EntityFrameworkCore;
using MyTripApi.Models.Entities;

namespace MyTripApi.Data
{
    public class MyTripDbContext : DbContext
    {
        public MyTripDbContext(DbContextOptions<MyTripDbContext> options) : base(options)
        {
        }

        public DbSet<Trip> Trip { get; set; }
    }
}
