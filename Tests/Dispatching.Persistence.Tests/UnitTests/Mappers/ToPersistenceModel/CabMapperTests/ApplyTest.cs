using AutoFixture;
using Dispatching.Cabs;
using Dispatching.Persistence.Mappers.ToPersistanceModel;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dispatching.Persistence.Tests.UnitTests.Mappers.ToPersistenceModel.CabMapperTests
{
    [TestClass]
    public class ApplyTest
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
            // Arrange
            var actual = new PersistenceModel.Cab();

            // Act
            _sut.Apply(_itemToMap, actual);

            // Assert
            actual.Id.Should().Be(_itemToMap.Id.ToGuid());
        }

        [TestMethod]
        public void WhenLongitude_ShouldMapLongitude()
        {
            // Arrange
            var actual = new PersistenceModel.Cab();

            // Act
            _sut.Apply(_itemToMap, actual);

            // Assert
            actual.Longitude.Should().Be(_itemToMap.CurrentLocation.Longitude);
        }

        [TestMethod]
        public void WhenLatitude_ShouldMapLatitude()
        {
            // Arrange
            var actual = new PersistenceModel.Cab();

            // Act
            _sut.Apply(_itemToMap, actual);

            // Assert
            actual.Latitude.Should().Be(_itemToMap.CurrentLocation.Latitude);
        }
    }
}
