using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Dispatching.ReadModel.Configuration
{
    public static class Dependencies
    {
        public static IServiceCollection UseDispatchingReadModel(this IServiceCollection serviceCollection, string connectionstring)
        {
            return serviceCollection
                .AddDbContext<DispatchingReadDbContext>()
                .AddTransient((s) => CreateUseDispatchingBrokerContext(connectionstring))
                .AddTransient<ICabRideRepository, CabRideRepository>();
        }

        private static DispatchingReadDbContext CreateUseDispatchingBrokerContext(string connectionstring)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DispatchingReadDbContext>();
            optionsBuilder.UseSqlServer(connectionstring);
            return new DispatchingReadDbContext(optionsBuilder.Options);
        }
    }
}
