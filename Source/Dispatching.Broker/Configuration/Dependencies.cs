using Dispatching.Broker.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Dispatching.Broker.Configuration
{
    public static class Dependencies
    {
        public static IServiceCollection UseDispatchingBroker(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .UseDispatchingBroker<ReBusConfiguration>();
        }
        public static IApplicationBuilder UseDispatchingBroker(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseRebus();
            return applicationBuilder;
        }

        internal static IServiceCollection UseDispatchingBroker<T>(this IServiceCollection serviceCollection) where T : ReBusConfiguration, new()
        {
            return serviceCollection
                .AddTransient<IQueue, RebusQueue>()
                .AddTransient<DriveCustomerToTrainStationHandler>()
                .AddTransient<DroveCustomerToTrainStationHandler>()
                .AddTransient<Commands.Mappers.IDriveCustomerToTrainStationMapper, Commands.Mappers.DriveCustomerToTrainStationMapper>()
                .AddTransient<Events.Mappers.ToDomainModel.ICabRideMapper, Events.Mappers.ToDomainModel.CabRideMapper>()
                .AddTransient<Events.Mappers.ToReadModel.ICabRideMapper, Events.Mappers.ToReadModel.CabRideMapper>()
                .UseReBus<T>();
        }

        internal static IServiceCollection UseReBus<T>(this IServiceCollection serviceCollection) where T : ReBusConfiguration, new()
        {
            var config = new T();
            config.Configure(serviceCollection);
            return serviceCollection;
        }
    }
}
