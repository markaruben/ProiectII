using Microsoft.EntityFrameworkCore;
using WorldFly.API.Models;

namespace WorldFly.API.Data
{
    public class WorldFlyDbContext : DbContext
    {
        public WorldFlyDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<City> City { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<Flight> Flight { get; set; }

        public DbSet<Booking> Booking { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
          
        }

        public void EnableIdentityInsert(string tableName)
        {
            Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {tableName} ON");
        }

        public void DisableIdentityInsert(string tableName)
        {
            Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {tableName} OFF");
        }
    }
}
