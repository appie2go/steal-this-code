using Microsoft.EntityFrameworkCore;
using Dispatching.Persistence.PersistenceModel;

namespace Dispatching.Persistence
{
    internal class DispatchingDbContext : DbContext
    {
        public DispatchingDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Cab> Cabs { get; set; }
        public DbSet<Distance> Distances { get; set; }

        public DbSet<PersistenceModel.Location> Locations { get; set; }
    }
}
