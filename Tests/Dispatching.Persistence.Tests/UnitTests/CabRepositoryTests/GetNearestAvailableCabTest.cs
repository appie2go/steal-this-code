using AutoFixture;
using Dispatching.Cabs;
using Dispatching.Persistence.Mappers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatching.Persistence.Tests.UnitTests.CabRepositoryTests
{
    [TestClass]
    public class GetNearestAvailableCabTest
    {
        private readonly Fixture _fixture = new Fixture();

        private IMapToPersistenceModel<Cab, PersistenceModel.Cab> _domainModelMapper;
        private IMapToDomainModel<PersistenceModel.Cab, Cab> _persistenceModelMapper;

        private Location _location;
        private decimal _distance;
        private Guid _nearestCabId;

        [TestInitialize]
        public void Initialize()
        {
            _domainModelMapper = Substitute.For<IMapToPersistenceModel<Cab, PersistenceModel.Cab>>();
            _persistenceModelMapper = Substitute.For<IMapToDomainModel<PersistenceModel.Cab, Cab>>();

            _location = _fixture.Create<Location>();
            _distance = _fixture.Create<decimal>();
            _nearestCabId = _fixture.Create<Guid>();
        }

        [TestMethod]
        public async Task WhenLocation_ShouldReturnNearestCab()
        {
            // Arrange
            var dbContext = new DispatchingDbContextBuilder()
                .WithCustomerLocation(_location)
                .WithCab(_nearestCabId, _distance)
                .WithCab(_fixture.Create<Guid>(), _distance + _fixture.Create<decimal>())
                .Build();

            // Act
            using (dbContext)
            {
                var sut = new CabRepository(dbContext, _domainModelMapper, _persistenceModelMapper);
                await sut.GetNearestAvailableCab(_location);

                // Assert
                _persistenceModelMapper
                    .Received(1)
                    .Map(Arg.Is<PersistenceModel.Cab>(x => x.Id == _nearestCabId));
            }
        }

        [TestMethod]
        public async Task WhenLocation_ShouldReturnResultFromPersistenceModelMapper()
        {
            // Arrange
            var dbContext = new DispatchingDbContextBuilder()
                .WithCustomerLocation(_location)
                .WithCab(_nearestCabId, _distance)
                .Build();

            var expected = _fixture.Create<Cab>();
            _persistenceModelMapper
                .Map(Arg.Any<PersistenceModel.Cab>())
                .Returns(expected);

            // Act
            using (dbContext)
            {
                var sut = new CabRepository(dbContext, _domainModelMapper, _persistenceModelMapper);
                var actual = await sut.GetNearestAvailableCab(_location);

                // Assert
                actual.Should().Be(expected);
            }
        }

        [TestMethod]
        public async Task WhenData_ShouldMapToDomainModel()
        {
            // Arrange
            var dbContext = new DispatchingDbContextBuilder(_fixture.Create<string>())
                .WithCustomerLocation(_location)
                .WithCab(_nearestCabId, _distance)
                .Build();

            // Act
            using (dbContext)
            {
                var sut = new CabRepository(dbContext, _domainModelMapper, _persistenceModelMapper);
                await sut.GetNearestAvailableCab(_location);

                // Assert
                _persistenceModelMapper
                    .Received(1)
                    .Map(Arg.Is(dbContext.Cabs.Single()));
            }
        }
    }
}
