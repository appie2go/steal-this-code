using AutoFixture;
using Dispatching.Aaa.Configuration;
using Dispatching.Api.Configuration;
using Dispatching.Broker.Configuration;
using Dispatching.Persistence.Configuration;
using Dispatching.ReadModel.Configuration;
using Dispatching.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System;
using TechTalk.SpecFlow;

namespace Dispatching.Specifications.TestContext
{
    internal class TestContext
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly IServiceCollection _serviceCollection;

        private readonly ScenarioContext _context;

        public TestContext(IServiceCollection serviceCollection, ScenarioContext context)
        {
            context.Initialize();

            _serviceCollection = serviceCollection;
            _context = context;

            _serviceCollection
                .UseDispatching()
                .UseDispatchingRestApi()
                .UseDispatchingBroker()
                .UseAaaTrafficInformation()
                .UseDispatchingPersistenceAdapters(_fixture.Create<string>())
                .UseDispatchingReadModel(_fixture.Create<string>())
                .AddControllers();
        }

        public TestContext Without<T>() where T : class
        {
            _serviceCollection.AddTransient((s) => Substitute.For<T>());
            return this;
        }

        public TestContext WithTestCase<T>() where T : TestCase, new()
        {
            var testCase = new T();
            testCase.Apply(_context);
            return this;
        }

        public TestContext WithTestCase<T>(TestCase<T> testCase) where T : class
        {
            testCase.Apply(_serviceCollection);
            return this;
        }

        public IServiceProvider Create()
        {
            var dbContextFactory = new InMemoryDbContexFactory(_context);

            return _serviceCollection
                .AddTransient((s) => dbContextFactory.CreateReadContext())
                .AddTransient((s) => dbContextFactory.CreateWriteContext())
                .BuildServiceProvider();
        }
    }
}
