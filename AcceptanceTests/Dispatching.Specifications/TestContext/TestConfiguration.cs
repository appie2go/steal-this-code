using Dispatching.Broker.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Config;
using Rebus.Persistence.InMem;
using Rebus.Subscriptions;
using Rebus.Transport;
using Rebus.Transport.InMem;

namespace Dispatching.Specifications.TestContext
{
    public class TestConfiguration : ReBusConfiguration
    {
        protected override void ConfigureTransport(StandardConfigurer<ITransport> configurer, string queueName)
        {
            configurer.UseInMemoryTransport(new InMemNetwork(), queueName);
        }

        protected override void ConfigureSubscriptions(StandardConfigurer<ISubscriptionStorage> configurer)
        {
            configurer.StoreInMemory();
        }
    }
}
