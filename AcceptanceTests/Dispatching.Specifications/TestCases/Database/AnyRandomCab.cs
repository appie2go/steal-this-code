using AutoFixture;
using Dispatching.Persistence;
using Dispatching.ReadModel;
using Dispatching.Specifications.TestContext;
using System;

namespace Dispatching.Specifications.TestCases.Database
{
    internal class AnyRandomCab : TestCase
    {
        private readonly Fixture _fixture = new Fixture();

        private Location _location;

        public AnyRandomCab()
        {
            _location = _fixture.Create<Location>();
        }

        public AnyRandomCab At(Location location)
        {
            _location = location;
            return this;
        }

        protected override void Apply(DispatchingDbContext dispatchingDbContext)
        {
            var distance = _fixture.Create<int>();
            var cab = _fixture.Create<Persistence.PersistenceModel.Cab>();
            cab.Latitude = _location.Latitude;
            cab.Longitude = _location.Longitude;
            dispatchingDbContext.Cabs.Add(cab);

            dispatchingDbContext.SaveChanges();
        }

        protected override void Apply(DispatchingReadDbContext dispatchingDbContext)
        {
            // This case doesn't need any read data
        }
    }
}
