using AutoFixture;
using Dispatching.TestFixtures.DomainObjects;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dispatching.Tests.UnitTests.LocationTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class InequalityTest
    {
        private readonly Fixture _fixture = new Fixture();

        [TestInitialize]
        public void Initialize()
        {
            _fixture.Customize(new LocationCustomization());
        }

        [TestMethod]
        public void WhenSame_ComparisonShouldBeFalse()
        {
            // Arrange
            var location = _fixture.Create<Location>();
            var sameLocation = new Location(location.Longitude, location.Latitude);

            // Act
            var actual = location != sameLocation;

            // Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void WhenNotSame_ComparisonShouldBeTrue()
        {
            // Arrange
            var location = _fixture.Create<Location>();
            var otherLocation = _fixture.Create<Location>();

            // Act
            var actual = location != otherLocation;

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenReferenceToSameObject_ComparisonShouldBeFalse()
        {
            // Arrange
            var location = _fixture.Create<Location>();

            // Act
#pragma warning disable CS1718 // Comparison made to same variable
            var actual = location != location;
#pragma warning disable CS1718 // Comparison made to same variable

            // Assert
            actual.Should().BeFalse();
        }
    }
}
