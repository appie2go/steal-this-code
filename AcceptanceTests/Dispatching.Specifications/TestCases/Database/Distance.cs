using AutoFixture;
using Dispatching.Persistence;
using Dispatching.ReadModel;
using Dispatching.Specifications.TestContext;
using System;

namespace Dispatching.Specifications.TestCases.Database
{
    internal class Distance : TestCase
    {
        private readonly Fixture _fixture = new Fixture();

        private readonly Location _a;
        private readonly Location _b;
        private int _distance;

        public Distance(Location a, Location b)
        {
            _a = a;
            _b = b;
        }

        public Distance WithDistance(int distance)
        {
            _distance = distance;
            return this;
        }

        protected override void Apply(DispatchingDbContext dispatchingDbContext)
        {
            dispatchingDbContext.Distances.Add(new Persistence.PersistenceModel.Distance
            {
                ToLongitude = _a.Longitude,
                ToLatitude = _a.Latitude,
                FromLatitude = _b.Latitude,
                FromLongitude = _b.Longitude,
                Id = _fixture.Create<Guid>(),
                Kilometers = _distance
            });

            dispatchingDbContext.Distances.Add(new Persistence.PersistenceModel.Distance
            {
                ToLongitude = _b.Longitude,
                ToLatitude = _b.Latitude,
                FromLatitude = _a.Latitude,
                FromLongitude = _a.Longitude,
                Id = _fixture.Create<Guid>(),
                Kilometers = _distance
            });

            dispatchingDbContext.SaveChanges();
        }

        protected override void Apply(DispatchingReadDbContext dispatchingDbContext)
        {

        }
    }
}
