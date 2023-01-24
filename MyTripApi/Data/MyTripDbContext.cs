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
        public DbSet<ToDoBeforeTrip> ToDoBeforeTrip { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(entity => {
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
            });
        }
    }
}
