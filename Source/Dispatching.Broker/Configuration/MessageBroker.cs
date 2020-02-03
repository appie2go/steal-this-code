using Dispatching.Broker.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Rebus.ServiceProvider;
using Rebus.Subscriptions;
using Rebus.Transport;
using System;

namespace Dispatching.Broker.Configuration
{
    public class MessageBroker : IDisposable
    {
        private BuiltinHandlerActivator _activator;
        private readonly IServiceCollection _serviceCollection;
        private readonly IServiceProvider _serviceProvider;

        protected virtual string QueueName { get => "Dispatching"; }

        public MessageBroker(IServiceCollection serviceCollection)
        {
            _activator = new BuiltinHandlerActivator();
            _serviceCollection = serviceCollection;

            RegisterHandlers(_activator);

            Configure.With(_activator)
                    .Transport((c) => ConfigureTransport(c, QueueName))
                    .Subscriptions(ConfigureSubscriptions)
                    .Routing(r => r.TypeBased().MapAssemblyOf<IQueue>(QueueName))
                    .Start();

            _serviceCollection.AddTransient((s) => _activator.Bus);

            _serviceProvider = _serviceCollection.BuildServiceProvider();
            _serviceProvider.UseRebus();
        }


        public T GetService<T>()
        {
            return _serviceProvider.GetService<T>();
        }

        protected virtual void ConfigureTransport(StandardConfigurer<ITransport> configurer, string queueName)
        {
            throw new NotImplementedException("Todo...");
        }

        protected virtual void ConfigureSubscriptions(StandardConfigurer<ISubscriptionStorage> configurer)
        {
            throw new NotImplementedException("Todo...");
        }

        protected void RegisterHandlers(BuiltinHandlerActivator activator)
        {
            activator.Register((x) => _serviceProvider.GetService<DriveCustomerToTrainStationHandler>());
            activator.Register((x) => _serviceProvider.GetService<DroveCustomerToTrainStationHandler>());
        }

        public void Dispose()
        {
            _activator.Dispose();
        }
    }
}
