using Dispatching.Broker.Commands;
using Dispatching.Broker.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Persistence.InMem;
using Rebus.Routing.TypeBased;
using Rebus.ServiceProvider;
using Rebus.Transport.InMem;
using System;

namespace Dispatching.Specifications.TestContext
{
    public static class RebusTestConfiguration
    {
        public static IServiceCollection ConfigureRebusTestSetup(this IServiceCollection serviceCollection)
        {
           

            return serviceCollection;
        }

        public static IServiceProvider UseRebusTestSetup(this IServiceProvider serviceProvider)
        {
            return serviceProvider.UseRebus();
        }
    }
}
