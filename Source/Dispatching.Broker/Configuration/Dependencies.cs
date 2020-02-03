using Dispatching.Broker.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Dispatching.Broker.Configuration
{
    public static class Dependencies
    {
        public static IServiceCollection UseDispatchingBroker(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddTransient<IQueue, RebusQueue>()
                .AddTransient<DriveCustomerToTrainStationHandler>()
                .AddTransient<DroveCustomerToTrainStationHandler>()
                .AddTransient<Commands.Mappers.IDriveCustomerToTrainStationMapper, Commands.Mappers.DriveCustomerToTrainStationMapper>()
                .AddTransient<Events.Mappers.ToDomainModel.ICabRideMapper, Events.Mappers.ToDomainModel.CabRideMapper>()
                .AddTransient<Events.Mappers.ToReadModel.ICabRideMapper, Events.Mappers.ToReadModel.CabRideMapper>();
        }
    }
}
