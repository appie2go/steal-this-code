using Microsoft.Extensions.DependencyInjection;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Rebus.ServiceProvider;
using Rebus.Subscriptions;
using Rebus.Transport;
using System;

namespace Dispatching.Broker.Configuration
{
    public class ReBusConfiguration
    {
        protected virtual string QueueName { get => "Dispatching"; }

        internal void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AutoRegisterHandlersFromAssemblyOf<IQueue>();
            serviceCollection.AddRebus(x => x
                    .Transport((c) => ConfigureTransport(c, QueueName))
                    .Subscriptions(ConfigureSubscriptions)
                    .Routing(r => r.TypeBased().MapAssemblyOf<IQueue>(QueueName))
            );
        }

        protected virtual void ConfigureTransport(StandardConfigurer<ITransport> configurer, string queueName)
        {
            throw new NotImplementedException("Todo...");
        }

        protected virtual void ConfigureSubscriptions(StandardConfigurer<ISubscriptionStorage> configurer)
        {
            throw new NotImplementedException("Todo...");
        }
    }
}
