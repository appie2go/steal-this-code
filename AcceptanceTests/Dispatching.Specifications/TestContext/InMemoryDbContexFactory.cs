using Dispatching.Persistence;
using Dispatching.ReadModel;
using Microsoft.EntityFrameworkCore;
using TechTalk.SpecFlow;

namespace Dispatching.Specifications.TestContext
{
    internal class InMemoryDbContexFactory
    {
        private readonly string _databaseName;

        public InMemoryDbContexFactory(ScenarioContext scenarioContext)
        {
            _databaseName = scenarioContext.GetDatabaseName();
        }

        public DispatchingDbContext CreateWriteContext()
        {
            var databaseName = $"{_databaseName}{nameof(DispatchingDbContext)}";
            var options = CreateOptions<DispatchingDbContext>(databaseName);
            return new DispatchingDbContext(options);
        }

        public DispatchingReadDbContext CreateReadContext()
        {
            var databaseName = $"{_databaseName}{nameof(DispatchingReadDbContext)}";
            var options = CreateOptions<DispatchingReadDbContext>(databaseName);
            return new DispatchingReadDbContext(options);
        }

        private static DbContextOptions<T> CreateOptions<T>(string databaseName) where T : DbContext
        {
            var optionsBuilder = new DbContextOptionsBuilder<T>();
            return optionsBuilder
                .UseInMemoryDatabase(databaseName)
                .Options;
        }
    }
}
