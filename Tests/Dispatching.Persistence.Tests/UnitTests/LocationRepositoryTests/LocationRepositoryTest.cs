using AutoFixture;
using Dispatching.Persistence.Mappers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;

namespace Dispatching.Persistence.Tests.UnitTests.LocationRepositoryTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class LocationRepositoryTest
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
        public void WhenContextNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            DispatchingDbContext context = null;

            // Act
            Action act = () => new LocationRepository(context, _persistenceModelMapper);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void WhenMapperNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            IMapToDomainModel<PersistenceModel.Location, Location> persistenceModelMapper = null;

            // Act
            Action act = () => new LocationRepository(_context, persistenceModelMapper);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }
    }
}
