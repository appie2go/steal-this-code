using Dispatching.Persistence.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dispatching.Persistence.Configuration
{
    public static class Dependencies
    {
        public static IServiceCollection UseDispatchingPersistenceAdapters(this IServiceCollection serviceCollection, string connectionstring)
        {
            return serviceCollection
                .AddDbContext<DispatchingDbContext>()
                .AddTransient((s) => CreateDispatchingDbContext(connectionstring))
                .AddTransient<IMapToDomainModel<PersistenceModel.Location, Location>, Mappers.ToDomainModel.LocationMapper>()
                .AddTransient<IMapToDomainModel<PersistenceModel.Cab, Cabs.Cab>, Mappers.ToDomainModel.CabMapper>()
                .AddTransient<IMapToPersistenceModel<Cabs.Cab, PersistenceModel.Cab>, Mappers.ToPersistanceModel.CabMapper>();
        }

        private static DispatchingDbContext CreateDispatchingDbContext(string connectionstring)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DispatchingDbContext>();
            optionsBuilder.UseSqlServer(connectionstring);
            return new DispatchingDbContext(optionsBuilder.Options);
        }
    }
}
