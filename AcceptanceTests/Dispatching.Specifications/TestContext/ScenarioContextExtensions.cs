using AutoFixture;
using Dispatching.Persistence;
using Dispatching.ReadModel;
using Microsoft.EntityFrameworkCore;
using System;
using TechTalk.SpecFlow;

namespace Dispatching.Specifications.TestContext
{
    internal static class ScenarioContextExtensions
    {
        public const string ScenarioKey = "testrunid";

        public static void Initialize(this ScenarioContext context)
        {
            var fixture = new Fixture();
            context[ScenarioKey] = fixture.Create<string>();
        }

        public static DispatchingDbContext CreateWriteDbContext(this ScenarioContext context)
        {
            var options = ConfigureInMemoryDbContext<DispatchingDbContext>(context);
            return new DispatchingDbContext(options);
        }

        public static DispatchingReadDbContext CreateReadDbContext(this ScenarioContext context)
        {
            var options = ConfigureInMemoryDbContext<DispatchingReadDbContext>(context);
            return new DispatchingReadDbContext(options);
        }

        private static DbContextOptions<T> ConfigureInMemoryDbContext<T>(ScenarioContext context) where T : DbContext
        {
            var testrunId = GetDatabaseName(context);
            var databaseName = $"{testrunId}{nameof(DispatchingReadDbContext)}";

            var optionsBuilder = new DbContextOptionsBuilder<T>();
            return optionsBuilder
                .UseInMemoryDatabase(databaseName)
                .Options;
        }

        private static string GetDatabaseName(ScenarioContext scenarioContext)
        {
            if (!scenarioContext.ContainsKey(ScenarioKey))
            {
                throw new InvalidOperationException(
                    $"Cannot construct a db context for the acceptance tests if the scenario " +
                    $"context does not contain a key named '{ScenarioKey}' that contains a string " +
                    $"value. Make sure it's set before the scenario starts."
                );
            }

            return (string)scenarioContext[ScenarioKey];
        }
    }
}
