using Dispatching.Rides.Processes;
using Dispatching.Rides.Processes.PrimaryPorts;
using Microsoft.Extensions.DependencyInjection;

namespace Dispatching.Configuration
{
    public static class Dependencies
    {
        public static IServiceCollection UseDispatching(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddTransient<ICabRideService, CabRideService>();
        }
    }
}
