using AutoFixture;
using Dispatching.Persistence;
using Dispatching.ReadModel;
using Dispatching.Specifications.TestContext;

namespace Dispatching.Specifications.TestCases.Database
{
    internal class AnyRandomTrainStation : TestCase
    {
        private readonly Fixture _fixture = new Fixture();

        private string _name;
        private Location _location;

        public AnyRandomTrainStation()
        {
            _name = _fixture.Create<string>();
            _location = _fixture.Create<Location>();
        }

        public AnyRandomTrainStation WithName(string name)
        {
            _name = name;
            return this;
        }

        public AnyRandomTrainStation At(Location location)
        {
            _location = location;
            return this;
        }

        protected override void Apply(DispatchingDbContext dispatchingDbContext)
        {
            var location = _fixture.Create<Persistence.PersistenceModel.Location>();
            location.Name = _name;
            location.Latitude = _location.Latitude;
            location.Longitude = _location.Longitude;
            dispatchingDbContext.Locations.Add(location);
            
            dispatchingDbContext.SaveChanges();
        }

        protected override void Apply(DispatchingReadDbContext dispatchingDbContext)
        {
            // This case doesn't need any read data
        }
    }
}
