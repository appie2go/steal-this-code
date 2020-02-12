using Dispatching.Broker;
using Dispatching.Persistence;
using Dispatching.ReadModel;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Rebus.ServiceProvider;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dispatching.Specifications.TestContext
{
    internal class Context
    {
        private const int Timeout = 25000;

        private readonly AutoResetEvent _testCompletedEvent = new AutoResetEvent(false);

        private IServiceProvider _serviceProvider;
        private ICallback _callback;

        public Context(IServiceCollection serviceCollection)
        {
            // Wait untill the call has been completed
            _callback = Substitute.For<ICallback>();
            _callback.When(x => x.CallBack(Arg.Any<Guid>())).Do((x) => Complete());
            serviceCollection.AddTransient((s) => _callback);

            // Fire up the application
            _serviceProvider = serviceCollection.BuildServiceProvider();
            _serviceProvider.UseRebus();
        }

        private void Complete()
        {
            _testCompletedEvent.Set();
        }

        private void WaitUntilCompleted()
        {
            _testCompletedEvent.WaitOne();
        }

        public DispatchingDbContext GetWriteDbContext()
        {
            return _serviceProvider.GetService<DispatchingDbContext>();
        }

        public DispatchingReadDbContext GetReadDbContext()
        {
            return _serviceProvider.GetService<DispatchingReadDbContext>();
        }

        public async Task Invoke<T>(Func<T, Task> action)
        {
            var sut = _serviceProvider.GetService<T>();
            await action(sut);
            WaitUntilCompleted();
        }
    }
}
