using AutoFixture;
using Dispatching.Cabs;
using Dispatching.Configuration;
using Dispatching.Persistence.Configuration;
using Dispatching.Rides.Processes.SecondaryPorts;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Dispatching.Persistence.Tests.ComponentTests
{
    [TestClass]
    [TestCategory("Component/Integration")]
    public class CabRepositoryTest
    {
        private readonly Fixture _fixture = new Fixture();

        private IProvideCab _sut;

        private DispatchingDbContext _context;

        [TestInitialize]
        public void Initialize()
        {
            _context = new InMemoryDispatchingDbContext(_fixture.Create<string>());

            // Bootstrap
            var serviceProvider = new ServiceCollection()
                .UseDispatching()
                .UseDispatchingPersistenceAdapters(_fixture.Create<string>())
                .AddTransient((s) => _context)
                .BuildServiceProvider();

            _sut = serviceProvider.GetService<IProvideCab>();
        }

        [TestMethod]
        public async Task WhenNewCab_ShouldPersistCab()
        {
            // Arrange
            var location = _fixture.Create<Location>();
            var cab = _fixture.Create<Cab>();

            AddRecordsToLookupTable(cab.CurrentLocation, location);

            // Act
            await _sut.Update(cab);
            var itemInDatabase = await _sut.GetNearestAvailableCab(location);

            // Assert
            itemInDatabase.Should().Be(cab);
        }

        private void AddRecordsToLookupTable(Location from, Location to)
        {
            var distance = _fixture.Create<PersistenceModel.Distance>();
            distance.FromLatitude = from.Latitude;
            distance.FromLongitude = from.Longitude;
            distance.ToLatitude = to.Latitude;
            distance.ToLongitude = to.Longitude;
            _context.Distances.Add(distance);

            var inverse = _fixture.Create<PersistenceModel.Distance>();
            inverse.FromLatitude = to.Latitude;
            inverse.FromLongitude = to.Longitude;
            inverse.ToLatitude = from.Latitude;
            inverse.ToLongitude = from.Longitude;
            _context.Distances.Add(inverse);

            _context.SaveChanges();
        }
    }
}
