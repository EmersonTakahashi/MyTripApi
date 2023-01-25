using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyTripApi.Models.Entities;

namespace MyTripApi.Data
{
    public class MyTripDbContext : IdentityDbContext<ApiUser>
    {
        public MyTripDbContext(DbContextOptions<MyTripDbContext> options) : base(options)
        {
        }

        public DbSet<Trip> Trip { get; set; }
        public DbSet<ToDoBeforeTrip> ToDoBeforeTrip { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}
