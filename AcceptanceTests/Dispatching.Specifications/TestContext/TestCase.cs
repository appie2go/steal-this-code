using Dispatching.Persistence;
using Dispatching.ReadModel;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
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
        public void Apply(IServiceCollection serviceCollection)
        {
            var substitute = Substitute.For<T>();
            Apply(substitute);
            serviceCollection.AddTransient((s) => substitute);
        }

        protected abstract void Apply(T substitute);
    }
}
