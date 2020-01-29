using AutoFixture;
using Dispatching.Broker.Commands;
using Dispatching.Broker.Commands.Mappers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dispatching.Broker.Tests.UnitTests.Commands.Mappers.DriveCustomerToTrainStationMapperTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class MapToCustomerLocationTest
    {
        private Fixture _fixture = new Fixture();

        [TestMethod]
        public void WhenCurrentLongitude_ShouldMapToLongitude()
        {
            // Arrange
            var input = _fixture.Create<DriveCustomerToTrainStation>();

            // Act
            var sut = new DriveCustomerToTrainStationMapper();
            var actual = sut.MapToCustomerLocation(input);

            // Assert
            actual.Longitude.Should().Be(input.CurrentLongitude);
        }


        [TestMethod]
        public void WhenCurrentLatitude_ShouldMapToLatitude()
        {
            // Arrange
            var input = _fixture.Create<DriveCustomerToTrainStation>();

            // Act
            var sut = new DriveCustomerToTrainStationMapper();
            var actual = sut.MapToCustomerLocation(input);

            // Assert
            actual.Latitude.Should().Be(input.CurrentLatitude);
        }
    }
}
