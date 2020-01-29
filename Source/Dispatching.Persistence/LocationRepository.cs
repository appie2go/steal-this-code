using Dispatching.Persistence.Mappers;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Dispatching.Persistence
{
    internal class LocationRepository : Rides.Processes.SecondaryPorts.IProvideLocation
    {
        private readonly DispatchingDbContext _context;
        private readonly IMapToDomainModel<PersistenceModel.Location, Location> _persistenceModelMapper;

        public LocationRepository(DispatchingDbContext context, 
            IMapToDomainModel<PersistenceModel.Location, Location> persistenceModelMapper) 
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _persistenceModelMapper = persistenceModelMapper ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Location> GetTrainStationLocation()
        {
            const string trainStationName = "Utrecht Centraal";

            try
            {
                var location = await _context.Locations
                    .Where(x => x.Name == trainStationName)
                    .SingleAsync();

                return _persistenceModelMapper.Map(location);
            }
            catch (InvalidOperationException e)
            {
                throw new KeyNotFoundException($"Database should contain a record in the Locations table with name '{trainStationName}'.", e);
            }
        }
    }
}
