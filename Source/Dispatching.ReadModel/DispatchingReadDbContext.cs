using Dispatching.ReadModel.PersistenceModel;
using Microsoft.EntityFrameworkCore;

namespace Dispatching.ReadModel
{
    internal class DispatchingReadDbContext : DbContext
    {
        public DispatchingReadDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<CabRide> CabRides { get; set; }
    }
}