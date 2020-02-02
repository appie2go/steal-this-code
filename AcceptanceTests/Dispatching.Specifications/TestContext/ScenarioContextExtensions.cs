using AutoFixture;
using System;
using TechTalk.SpecFlow;

namespace Dispatching.Specifications.TestContext
{
    public static class ScenarioContextExtensions
    {
        public const string ScenarioKey = "databasename";

        public static void Initialize(this ScenarioContext context)
        {
            var fixture = new Fixture();
            context[ScenarioKey] = fixture.Create<string>();
        }

        public static string GetDatabaseName(this ScenarioContext scenarioContext)
        {
            if (!scenarioContext.ContainsKey(ScenarioKey))
            {
                throw new InvalidOperationException($"Cannot construct a db context for the acceptance tests if the scenario context does not" +
                    $"contain a key named '{ScenarioKey}' that contains a string value. Make it's set before the scenario starts.");
            }

            return (string)scenarioContext[ScenarioKey];
        }
    }
}
