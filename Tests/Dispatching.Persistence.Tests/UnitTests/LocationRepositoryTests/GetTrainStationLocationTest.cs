using AutoFixture;
using Dispatching.Persistence.Mappers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dispatching.Persistence.Tests.UnitTests.LocationRepositoryTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class GetTrainStationLocationTest
    {
        private readonly Fixture _fixture = new Fixture();

        private DispatchingDbContext _context;
        private IMapToDomainModel<PersistenceModel.Location, Location> _persistenceModelMapper;

        [TestInitialize]
        public void Initialize()
        {
            _context = new InMemoryDispatchingDbContext(_fixture.Create<string>());
            _persistenceModelMapper = Substitute.For<IMapToDomainModel<PersistenceModel.Location, Location>>();
        }

        [TestMethod]
        public async Task WhenUtrechtCentraalExists_ShouldMapLocation()
        {
            // Arrange
            var location = _fixture.Create<PersistenceModel.Location>();
            location.Name = "Utrecht Centraal";

            using (_context)
            {
                _context.Locations.Add(location);
                _context.SaveChanges();

                // Act
                var sut = new LocationRepository(_context, _persistenceModelMapper);
                await sut.GetTrainStationLocation();

                // Assert
                _persistenceModelMapper
                    .Received(1)
                    .Map(Arg.Is(location));
            }
        }

        [TestMethod]
        public async Task WhenUtrechtCentraalExists_ShouldReturnWhateverTheMapperReturns()
        { 
            // Arrange
            var location = _fixture.Create<PersistenceModel.Location>();
            location.Name = "Utrecht Centraal";

            var expected = _fixture.Create<Location>();
            _persistenceModelMapper
                .Map(Arg.Any<PersistenceModel.Location>())
                .Returns(expected);

            using (_context)
            {
                _context.Locations.Add(location);
                _context.SaveChanges();

                // Act
                var sut = new LocationRepository(_context, _persistenceModelMapper);
                var actual = await sut.GetTrainStationLocation();

                // Assert
                actual.Should().Be(expected);
            }
        }

        [TestMethod]
        public async Task WhenUtrechtCentraalDoesNotExist_ShouldThrowKeyNotFoundException()
        {
            using (_context)
            {
                // Act
                var sut = new LocationRepository(_context, _persistenceModelMapper);
                Func<Task> act = async () => await sut.GetTrainStationLocation();

                // Assert
                act.Should()
                    .Throw<KeyNotFoundException>("Database should contain a record in the Locations table with name 'Utrecht Centraal'.");
            }
        }
    }
}
