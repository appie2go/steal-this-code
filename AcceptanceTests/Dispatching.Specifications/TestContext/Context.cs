using Dispatching.Broker;
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
        private const int Timeout = 2500;

        private readonly AutoResetEvent _testCompletedEvent = new AutoResetEvent(true);

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

        public async Task Invoke<T>(Func<T, Task> action)
        {
            var sut = _serviceProvider.GetService<T>();

            var errored = false;
            var timer = new System.Timers.Timer();
            timer.Interval = Timeout;
            timer.Elapsed += (x, y) => Complete();

            timer.Start();
            
            await action(sut);
            WaitUntilCompleted();
        }
    }
}
