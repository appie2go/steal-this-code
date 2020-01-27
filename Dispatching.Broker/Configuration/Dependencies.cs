using Microsoft.Extensions.DependencyInjection;

namespace Dispatching.Broker.Configuration
{
    public static class Dependencies
    {
        public static IServiceCollection UseDispatchingBroker(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddTransient<IQueue, RebusQueue>()
                .AddTransient<Commands.Mappers.IDriveCustomerToTrainStationMapper, Commands.Mappers.DriveCustomerToTrainStationMapper>()
                .AddTransient<Events.Mappers.DomainModel.ICabRideMapper, Events.Mappers.DomainModel.CabRideMapper>()
                .AddTransient<Events.Mappers.ReadModel.ICabRideMapper, Events.Mappers.ReadModel.CabRideMapper>();
        }

        // Todo
        //public static IApplicationBuilder UseBroker(this IApplicationBuilder applicationBuilder)
        //{
        //    app.ApplicationServices.UseRebus();
        //}
    }
}
