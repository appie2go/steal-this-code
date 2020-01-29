using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoFixture;
using FluentAssertions;

namespace Dispatching.Tests.UnitTests.LocationTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class LocationTest
    {
        private readonly Fixture _fixture = new Fixture();

        [TestMethod]
        public void WhenDefault_LatitudeShouldBe0()
        {
            // Arrange
            var location = new Location();

            // Act
            var actual = location.Latitude;

            // Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void WhenDefault_LongitudeShouldBe0()
        {
            // Arrange
            var location = new Location();

            // Act
            var actual = location.Longitude;

            // Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void WhenLongitudeProvided_LongitudeShouldBeSet()
        {
            // Arrange
            var longitude = _fixture.Create<decimal>();

            // Act
            var location = new Location(longitude, _fixture.Create<decimal>());
            var actual = location.Longitude;

            // Assert
            actual.Should().Be(longitude);
        }

        [TestMethod]
        public void WhenLatitudeProvide_LatitudeShouldBeSet()
        {
            // Arrange
            var latitude = _fixture.Create<decimal>();

            // Act
            var location = new Location(_fixture.Create<decimal>(), latitude);
            var actual = location.Latitude;

            // Assert
            actual.Should().Be(latitude);
        }
    }
}
