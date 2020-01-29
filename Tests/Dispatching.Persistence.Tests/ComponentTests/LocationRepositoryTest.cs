using AutoFixture;
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
    public class LocationRepositoryTest
    {
        private readonly Fixture _fixture = new Fixture();

        private IProvideLocation _sut;

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

            _sut = serviceProvider.GetService<IProvideLocation>();
        }

        [TestMethod]
        public async Task WhenTrainstationExistsInDatabase_ShouldReturnLocation()
        {
            // Arrange
            var location = _fixture.Create<PersistenceModel.Location>();
            location.Name = "Utrecht Centraal";

            _context.Locations.Add(location);
            _context.SaveChanges();

            // Act
            var actual = await _sut.GetTrainStationLocation();

            // Assert
            actual.Should().NotBeNull();
        }
    }
}
