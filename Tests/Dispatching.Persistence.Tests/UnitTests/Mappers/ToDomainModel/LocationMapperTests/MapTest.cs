using AutoFixture;
using FluentAssertions;
using Dispatching.Persistence.Mappers.ToDomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dispatching.Persistence.Tests.UnitTests.Mappers.ToDomainModel.LocationMapperTests
{
    [TestClass]
    public class MapTest
    {
        private readonly Fixture _fixture = new Fixture();

        private LocationMapper _sut;

        private PersistenceModel.Location _itemToMap;

        [TestInitialize]
        public void Initialize()
        {
            _sut = new LocationMapper();
            _itemToMap = _fixture.Create<PersistenceModel.Location>();
        }

        [TestMethod]
        public void WhenLongitude_ShouldMapLongitude()
        {
            // Act
            var actual = _sut.Map(_itemToMap);

            // Assert
            actual.Longitude.Should().Be(_itemToMap.Longitude);
        }

        [TestMethod]
        public void WhenLatitude_ShouldMapLatitude()
        {
            // Act
            var actual = _sut.Map(_itemToMap);

            // Assert
            actual.Latitude.Should().Be(_itemToMap.Latitude);
        }
    }
}
