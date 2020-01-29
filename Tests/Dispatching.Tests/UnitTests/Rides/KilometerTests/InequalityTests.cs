using AutoFixture;
using Dispatching.Rides;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dispatching.Tests.UnitTests.Rides.KilometerTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class InequalityTests
    {
        private readonly Fixture _fixture = new Fixture();

        private decimal _distance;

        [TestInitialize]
        public void Initialize()
        {
            _distance = _fixture.Create<decimal>();
        }

        [TestMethod]
        public void WhenSameDistance_ComparisonShouldBeFalse()
        {
            // Arrange
            var instance1 = Kilometer.FromDecimal(_distance);
            var instance2 = Kilometer.FromDecimal(_distance);

            // Act
            var actual = instance1 != instance2;

            // Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void WhenDifferentDistances_ComparisonShouldBeTrue()
        {
            // Arrange
            var instance1 = Kilometer.FromDecimal(_distance);
            var instance2 = Kilometer.FromDecimal(_fixture.Create<decimal>());

            // Act
            var actual = instance1 != instance2;

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenSameInstance_ComparisonShouldBeTrue()
        {
            // Arrange
            var instance = Kilometer.FromDecimal(_distance);

            // Act
            var actual = instance != instance;

            // Assert
            actual.Should().BeFalse();
        }
    }
}
