using Dispatching.ReadModel;
using Microsoft.EntityFrameworkCore;

namespace Dispatching.Persistence.Tests
{
    internal class InMemoryDispatchingDbContext : DispatchingReadDbContext
    {
        public InMemoryDispatchingDbContext(string databaseName) : base(CreateOptions(databaseName))
        {

        }

        private static DbContextOptions<DispatchingReadDbContext> CreateOptions(string databaseName)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DispatchingReadDbContext>();
            return optionsBuilder
                .UseInMemoryDatabase(databaseName)
                .Options;
        }
    }
}
