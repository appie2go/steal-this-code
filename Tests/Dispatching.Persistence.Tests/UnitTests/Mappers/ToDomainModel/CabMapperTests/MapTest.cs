using AutoFixture;
using FluentAssertions;
using Dispatching.Persistence.Mappers.ToDomainModel;
using Dispatching.Persistence.PersistenceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dispatching.Persistence.Tests.UnitTests.Mappers.ToDomainModel.CabMapperTests
{
    [TestClass]
    public class MapTest
    {
        private readonly Fixture _fixture = new Fixture();

        private CabMapper _sut;

        private Cab _itemToMap;

        [TestInitialize]
        public void Initialize()
        {
            _sut = new CabMapper();
            _itemToMap = _fixture.Create<Cab>();
        }

        [TestMethod]
        public void WhenId_ShouldMapId()
        {
            // Act
            var actual = _sut.Map(_itemToMap);

            // Assert
            actual.Id.ToGuid().Should().Be(_itemToMap.Id);
        }

        [TestMethod]
        public void WhenLongitude_ShouldMapLongitude()
        {
            // Act
            var actual = _sut.Map(_itemToMap);

            // Assert
            actual.CurrentLocation.Longitude.Should().Be(_itemToMap.Longitude);
        }

        [TestMethod]
        public void WhenLatitude_ShouldMapLatitude()
        {
            // Act
            var actual = _sut.Map(_itemToMap);

            // Assert
            actual.CurrentLocation.Latitude.Should().Be(_itemToMap.Latitude);
        }

        [TestMethod]
        public void WhenAny_ShouldHaveNoPassengers()
        {
            // Act
            var actual = _sut.Map(_itemToMap);

            // Assert
            actual.Passengers.Should().BeEmpty();
        }
    }
}
