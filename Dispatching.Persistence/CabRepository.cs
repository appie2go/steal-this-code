using Dispatching.Cabs;
using Dispatching.Persistence.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatching.Persistence
{
    internal class CabRepository : Repository<Cab, PersistenceModel.Cab>, Rides.Processes.SecondaryPorts.IProvideCab
    {
        private readonly DispatchingDbContext _context;
        private readonly IMapToDomainModel<PersistenceModel.Cab, Cab> _persistenceModelMapper;

        public CabRepository(DispatchingDbContext context,
            IMapToPersistenceModel<Cab, PersistenceModel.Cab> domainModelMapper,
            IMapToDomainModel<PersistenceModel.Cab, Cab> persistenceModelMapper) : base(context, domainModelMapper, persistenceModelMapper)
        {
            _context = context;
            _persistenceModelMapper = persistenceModelMapper;
        }

        public async Task<Cab> GetNearestAvailableCab(Location location)
        {
            var query = from c in _context.Cabs
                      join d in _context.Distances 
                          on new { 
                              FromLong = c.Longitude, 
                              FromLat = c.Latitude, 
                              ToLong = location.Longitude, 
                              ToLat = location.Latitude 
                          } 
                          equals new 
                          { 
                              FromLong = d.FromLongitude, 
                              FromLat = d.FromLatitude, 
                              ToLong = d.ToLongitude, 
                              ToLat = d.ToLatitude 
                          }
                      orderby d.Kilometers ascending
                      select c;

            var cab = await query.FirstAsync();

            return _persistenceModelMapper.Map(cab);
        }

        protected override async Task<PersistenceModel.Cab> Get(Guid id)
        {
            return await _context.Cabs
                .Where(x => x.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        protected override async Task AddAsync(PersistenceModel.Cab newEntity)
        {
            await _context.Cabs.AddAsync(newEntity);
        }
    }
}