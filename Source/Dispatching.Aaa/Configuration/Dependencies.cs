using Dispatching.Aaa.Mappers;
using Dispatching.Rides.Processes.SecondaryPorts;
using Microsoft.Extensions.DependencyInjection;

namespace Dispatching.Aaa.Configuration
{
    public static class Dependencies
    {
        public static IServiceCollection UseAaaTrafficInformation(this IServiceCollection serviceCollection) 
        {
            return serviceCollection
                .AddTransient<IEstimatedTimeOfArrivalRequestMapper, EstimatedTimeOfArrivalRequestMapper>()
                .AddTransient<IEstimatedTimeOfArrivalResponseMapper, EstimatedTimeOfArrivalResponseMapper>()
                .AddTransient<IGetDistanceRequestMapper, GetDistanceRequestMapper>()
                .AddTransient<IGetDistanceResponseMapper, GetDistanceResponseMapper>()
                .AddTransient<IProvideTrafficInformation, AaaServiceProxy>();
        }
    }
}
