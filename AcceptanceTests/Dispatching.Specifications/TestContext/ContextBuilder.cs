using AutoFixture;
using Dispatching.Aaa.Configuration;
using Dispatching.Api.Configuration;
using Dispatching.Broker.Configuration;
using Dispatching.Configuration;
using Dispatching.Persistence.Configuration;
using Dispatching.ReadModel.Configuration;
using Dispatching.Specifications.TestCases;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System;
using TechTalk.SpecFlow;

namespace Dispatching.Specifications.TestContext
{
    internal class ContextBuilder
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly IServiceCollection _serviceCollection = new ServiceCollection();

        private readonly ScenarioContext _context;

        public ContextBuilder(ScenarioContext context)
        {
            _context = context;
            _context.Initialize();

            _serviceCollection
                .UseDispatching()
                .UseDispatchingRestApi()
                .UseDispatchingBroker<TestConfiguration>() // Override default configuration
                .UseAaaTrafficInformation()
                .UseDispatchingPersistenceAdapters(_fixture.Create<string>())
                .UseDispatchingReadModel(_fixture.Create<string>())
                .AddControllers();

            With(new Clock());
        }

        public ContextBuilder SetTime(DateTime time)
        {
            With(new Clock().WithTime(time));
            return this;
        }

        public ContextBuilder Without<T>() where T : class
        {
            _serviceCollection.AddTransient((s) => Substitute.For<T>());
            return this;
        }

        public ContextBuilder Replace<T>(T replacement) where T : class
        {
            _serviceCollection.AddTransient((s) => replacement);
            return this;
        }

        public ContextBuilder With<T>() where T : TestCase, new()
        {
            var testCase = new T();
            testCase.Apply(_context);
            return this;
        }

        public ContextBuilder With(TestCase testCase)
        {
            testCase.Apply(_context);
            return this;
        }

        public ContextBuilder With<T>(TestCase<T> testCase) where T : class
        {
            testCase.Apply(_serviceCollection);
            return this;
        }

        public Context Build()
        {
            var serviceCollection = _serviceCollection
                .AddSingleton((s) => _context.CreateReadDbContext())
                .AddSingleton((s) => _context.CreateWriteDbContext());

            return new Context(serviceCollection);
        }
    }
}
