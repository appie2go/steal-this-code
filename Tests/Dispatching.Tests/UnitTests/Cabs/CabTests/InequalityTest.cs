using AutoFixture;
using Dispatching.Cabs;
using Dispatching.Framework;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dispatching.Tests.UnitTests.Cabs.CabTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class InequalityTest
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
        public void WhenSame_ComparisonShouldBeFalse()
        {
            // Arrange
            var cab = new Cab(_id, _location);
            var sameCab = new Cab(_id, _location);

            // Act
            var actual = cab != sameCab;

            // Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void WhenNotSame_ComparisonShouldBeTrue()
        {
            // Arrange
            var cab = _fixture.Create<Cab>();
            var otherCab = _fixture.Create<Cab>();

            // Act
            var actual = cab != otherCab;

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenReferenceToSameObject_ComparisonShouldBeTrue()
        {
            // Arrange
            var cab = _fixture.Create<Cab>();

            // Act
#pragma warning disable CS1718 // Comparison made to same variable
            var actual = cab != cab;
#pragma warning disable CS1718 // Comparison made to same variable

            // Assert
            actual.Should().BeFalse();
        }
    }
}
