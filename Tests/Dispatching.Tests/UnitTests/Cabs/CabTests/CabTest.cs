using AutoFixture;
using Dispatching.Cabs;
using Dispatching.Framework;
using Dispatching.TestFixtures.DomainObjects;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dispatching.Tests.UnitTests.Cabs.CabTests
{
    [TestClass]
    public class CabTest
    {
        private Fixture _fixture = new Fixture();

        private Id<Cab> _id;
        private Location _location;

        [TestInitialize]
        public void Initialize()
        {
            _fixture.Customize(new LocationCustomization());

            _id = _fixture.Create<Id<Cab>>();
            _location = _fixture.Create<Location>();
        }

        [TestMethod]
        public void WhenIdProvided_ShouldSetId()
        {
            // Arrange
            var id = _fixture.Create<Id<Cab>>();

            // Act
            var actual = new Cab(id, _location);

            // Assert
            actual.Id.Should().Be(id);
        }

        [TestMethod]
        public void WhenLocationProvided_ShouldSetLocation()
        {
            // Arrange
            var location = _fixture.Create<Location>();

            // Act
            var actual = new Cab(_id, location);

            // Assert
            actual.CurrentLocation.Should().Be(location);
        }
    }
}
