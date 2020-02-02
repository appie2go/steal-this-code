using Dispatching.Persistence;
using Dispatching.ReadModel;
using Microsoft.EntityFrameworkCore;
using System;
using TechTalk.SpecFlow;

namespace Dispatching.Specifications.TestContext
{
    internal class InMemoryDbContexFactory
    {
        public const string ScenarioKey = "databasename";

        private readonly string _databaseName;

        public InMemoryDbContexFactory(ScenarioContext scenarioContext)
        {            
            if (!scenarioContext.ContainsKey(ScenarioKey)) 
            {
                throw new InvalidOperationException($"Cannot construct a db context for the acceptance tests if the scenario context does not" +
                    $"contain a key named '{ScenarioKey}' that contains a string value. Make it's set before the scenario starts.");
            }

            _databaseName = (string)scenarioContext[ScenarioKey];
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
