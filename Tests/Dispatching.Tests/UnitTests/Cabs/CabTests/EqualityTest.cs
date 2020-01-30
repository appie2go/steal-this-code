using AutoFixture;
using Dispatching.Cabs;
using Dispatching.Framework;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dispatching.Tests.UnitTests.Cabs.CabTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class EqualityTest
    {
        private readonly Fixture _fixture = new Fixture();

        private Id<Cab> _id;
        private Location _location;

        [TestInitialize]
        public void Initialize()
        {
            _id = _fixture.Create<Id<Cab>>();
            _location = _fixture.Create<Location>();
        }

        [TestMethod]
        public void WhenSameId_ComparisonShouldBeTrue()
        {
            // Arrange
            var cab = new Cab(_id, _location);
            var sameCab = new Cab(_id, _fixture.Create<Location>());

            // Act
            var actual = cab == sameCab;

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenDifferentIds_ComparisonShouldBeFalse()
        {
            // Arrange
            var cab = new Cab(_id, _location);
            var otherCab = new Cab(_fixture.Create<Id<Cab>>(), _location);

            // Act
            var actual = cab == otherCab;

            // Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void WhenSameInstance_ComparisonShouldBeTrue()
        {
            // Arrange
            var cab = new Cab(_id, _location);

            // Act
#pragma warning disable CS1718 // Comparison made to same variable
            var actual = cab == cab;
#pragma warning restore CS1718 // Comparison made to same variable

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenSameId_ShouldBeEqual()
        {
            // Arrange
            var cab = new Cab(_id, _location);
            var sameCab = new Cab(_id, _fixture.Create<Location>());

            // Act
            var actual = cab.Equals(sameCab);

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenDifferentIds_ShouldNotBeEqual()
        {
            // Arrange
            var cab = new Cab(_id, _location);
            var otherCab = new Cab(_fixture.Create<Id<Cab>>(), _location);

            // Act
            var actual = cab.Equals(otherCab);

            // Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void WhenSameInstance_ShouldBeEqual()
        {
            // Arrange
            var cab = new Cab(_id, _location);

            // Act
            var actual = cab.Equals(cab);

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenCastedToObjectAndSameId_ShouldBeEqual()
        {
            // Arrange
            var cab = new Cab(_id, _location);
            var sameCab = new Cab(_id, _fixture.Create<Location>());

            // Act
            var actual = cab.Equals((object)sameCab);

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenCastedToObjectAndDifferentIds_ShouldNotBeEqual()
        {
            // Arrange
            var cab = new Cab(_id, _location);
            var otherCab = new Cab(_fixture.Create<Id<Cab>>(), _location);

            // Act
            var actual = cab.Equals((object)otherCab);

            // Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void WhenCastedToObjectAndSameInstance_ShouldBeEqual()
        {
            // Arrange
            var cab = new Cab(_id, _location);

            // Act
            var actual = cab.Equals((object)cab);

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenDifferentObjects_ShouldHaveDifferentHashcodes()
        {
            // Arrange
            var cab = _fixture.Create<Cab>();
            var otherCab = _fixture.Create<Cab>();

            // Act
            var actual1 = cab.GetHashCode();
            var actual2 = otherCab.GetHashCode();

            // Asset
            actual1.Should().NotBe(actual2);
        }
    }
}
