using AutoFixture;
using System;

namespace Dispatching.Persistence.Tests
{
    internal class DispatchingDbContextBuilder
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly DispatchingDbContext _dbContext;

        private Location _location;

        internal DispatchingDbContextBuilder() : this(Guid.NewGuid().ToString())
        {
        }

        internal DispatchingDbContextBuilder(string dbName)
        {
            _dbContext = new InMemoryDispatchingDbContext(dbName);
        }

        public DispatchingDbContextBuilder WithCustomerLocation(Location location)
        {
            _location = location;
            return this;
        }

        public DispatchingDbContextBuilder WithCab(PersistenceModel.Cab cab)
        {
            _dbContext.Cabs.Add(cab);
            return this;
        }

        public DispatchingDbContextBuilder WithCab(Guid cabId, decimal distance)
        {
            var cab = new PersistenceModel.Cab
            {
                Id = cabId,
                Latitude = _fixture.Create<decimal>(),
                Longitude = _fixture.Create<decimal>()
            };

            _dbContext.Cabs.Add(cab);

            _dbContext.Distances.Add(new PersistenceModel.Distance
            {
                Id = _fixture.Create<Guid>(),
                Kilometers = distance,
                ToLongitude = _location.Longitude,
                ToLatitude = _location.Latitude,
                FromLatitude = cab.Latitude,
                FromLongitude = cab.Longitude
            });

            return this;
        }

        public DispatchingDbContext Build()
        {
            _dbContext.SaveChanges();
            return _dbContext;
        }
    }
}
