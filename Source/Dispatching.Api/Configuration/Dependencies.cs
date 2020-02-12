using Dispatching.Api.Controllers;
using Dispatching.Api.ModelValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Dispatching.Api.Configuration
{
    public static class Dependencies
    {
        public static IServiceCollection UseDispatchingRestApi(this IServiceCollection collection)
        {
            return collection
                .AddTransient<CabRideController>()
                .AddTransient<DriveCustomerToTrainStationValidator>();
        }
    }
}
