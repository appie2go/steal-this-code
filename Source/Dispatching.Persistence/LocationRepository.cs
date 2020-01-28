using Dispatching.Persistence.Mappers;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Dispatching.Persistence
{
    internal class LocationRepository : Rides.Processes.SecondaryPorts.IProvideLocation
    {
        private readonly DispatchingDbContext _context;
        private readonly IMapToDomainModel<PersistenceModel.Location, Location> _persistenceModelMapper;

        public LocationRepository(DispatchingDbContext context, 
            IMapToDomainModel<PersistenceModel.Location, Location> persistenceModelMapper) 
        {
            _context = context;
            _persistenceModelMapper = persistenceModelMapper;
        }

        public async Task<Location> GetTrainStationLocation()
        {
            const string trainStationName = "Utrecht Centraal";
            var location = await _context.Locations
                .Where(x => x.Name == trainStationName)
                .SingleAsync();

            return _persistenceModelMapper.Map(location);
        }
    }
}
