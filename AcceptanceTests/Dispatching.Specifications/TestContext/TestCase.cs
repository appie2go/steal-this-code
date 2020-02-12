using Dispatching.Persistence;
using Dispatching.ReadModel;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace Dispatching.Specifications.TestContext
{
    internal abstract class TestCase
    {
        public void Apply(ScenarioContext context)
        {
            using (var dbContext = context.CreateWriteDbContext())
            {
                Apply(dbContext);
            }

            using (var readContext = context.CreateReadDbContext())
            {
                Apply(readContext);
            }
        }

        protected abstract void Apply(DispatchingDbContext dispatchingDbContext);
        protected abstract void Apply(DispatchingReadDbContext dispatchingDbContext);
    }

    internal abstract class TestCase<T> where T : class
    {
        private readonly List<TestCase<T>> _otherTestCases = new List<TestCase<T>>();

        public TestCase<T> AppendWith(TestCase<T> appendWith)
        {
            _otherTestCases.Add(appendWith);
            return this;
        }

        public void Apply(IServiceCollection serviceCollection)
        {
            var substitute = Substitute.For<T>();
            serviceCollection.AddTransient((s) => substitute);

            Apply(substitute);
            foreach (var testcase in _otherTestCases)
            {
                testcase.Apply(substitute);
            }

            serviceCollection.AddTransient((s) => substitute);
        }

        protected abstract void Apply(T substitute);
    }
}
