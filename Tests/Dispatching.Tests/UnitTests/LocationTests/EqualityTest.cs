using AutoFixture;
using FluentAssertions;
using Dispatching.TestFixtures.DomainObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dispatching.Tests.UnitTests.LocationTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class EqualityTest
    {
        private readonly Fixture _fixture = new Fixture();

        [TestInitialize]
        public void Initialize()
        {
            _fixture.Customize(new LocationCustomization());
        }

        [TestMethod]
        public void WhenSame_ComparisonShouldBeTrue()
        {
            // Arrange
            var location = _fixture.Create<Location>();
            var sameLocation = new Location(location.Longitude, location.Latitude);

            // Act
            var actual = location == sameLocation;

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenNotSame_ComparisonShouldBeFalse()
        {
            // Arrange
            var location = _fixture.Create<Location>();
            var otherLocation = _fixture.Create<Location>();

            // Act
            var actual = location == otherLocation;

            // Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void WhenReferenceToSameObject_ComparisonShouldBeTrue()
        {
            // Arrange
            var location = _fixture.Create<Location>();

            // Act
            var actual = location == location;

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenSame_ShouldBeEqual()
        {
            // Arrange
            var location = _fixture.Create<Location>();
            var sameLocation = new Location(location.Longitude, location.Latitude);

            // Act
            var actual = location.Equals(sameLocation);

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenNotSame_ShouldNotBeEqual()
        {
            // Arrange
            var location = _fixture.Create<Location>();
            var otherLocation = _fixture.Create<Location>();

            // Act
            var actual = location.Equals(otherLocation);

            // Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void WhenReferenceToSameObject_ShouldBeEqual()
        {
            // Arrange
            var location = _fixture.Create<Location>();

            // Act
            var actual = location.Equals(location);

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenCastedToObjectAndSame_ShouldBeEqual()
        {
            // Arrange
            var location = _fixture.Create<Location>();
            var sameLocation = new Location(location.Longitude, location.Latitude);

            // Act
            var actual = location.Equals((object)sameLocation);

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenCastedToObjectAndNotSame_ShouldNotBeEqual()
        {
            // Arrange
            var location = _fixture.Create<Location>();
            var otherLocation = _fixture.Create<Location>();

            // Act
            var actual = location.Equals((object)otherLocation);

            // Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void WhenCastedToObjectAndReferenceToSameObject_ShouldBeEqual()
        {
            // Arrange
            var location = _fixture.Create<Location>();

            // Act
            var actual = location.Equals((object)location);

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenDifferentObjects_ShouldHaveDifferentHashcodes()
        {
            // Arrange
            var location = _fixture.Create<Location>();
            var otherLocation = _fixture.Create<Location>();

            // Act
            var actual1 = location.GetHashCode();
            var actual2 = otherLocation.GetHashCode();

            // Asset
            actual1.Should().NotBe(actual2);
        }
    }
}
