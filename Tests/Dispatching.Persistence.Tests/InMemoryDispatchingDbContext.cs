using Microsoft.EntityFrameworkCore;

namespace Dispatching.Persistence.Tests
{
    internal class InMemoryDispatchingDbContext : DispatchingDbContext
    {
        public InMemoryDispatchingDbContext(string databaseName) : base(CreateOptions(databaseName))
        {

        }

        private static DbContextOptions<DispatchingDbContext> CreateOptions(string databaseName)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DispatchingDbContext>();
            return optionsBuilder
                .UseInMemoryDatabase(databaseName)
                .Options;
        }
    }
}
