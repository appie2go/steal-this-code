using AutoFixture;
using Dispatching.Rides;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dispatching.Tests.UnitTests.Rides.EuroTests
{
    [TestClass]
    public class InequalityTests
    {
        private readonly Fixture _fixture = new Fixture();

        private decimal _amount;

        [TestInitialize]
        public void Initialize()
        {
            _amount = _fixture.Create<decimal>();
        }

        [TestMethod]
        public void WhenSameAmounts_ComparisonShouldBeFalse()
        {
            // Arrange
            var instance1 = Euro.FromDecimal(_amount);
            var instance2 = Euro.FromDecimal(_amount);

            // Act
            var actual = instance1 != instance2;

            // Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void WhenDifferentAmounts_ComparisonShouldBeTrue()
        {
            // Arrange
            var instance1 = Euro.FromDecimal(_amount);
            var instance2 = Euro.FromDecimal(_fixture.Create<decimal>());

            // Act
            var actual = instance1 != instance2;

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenSameInstance_ComparisonShouldBeTrue()
        {
            // Arrange
            var instance = Euro.FromDecimal(_amount);

            // Act
            var actual = instance != instance;

            // Assert
            actual.Should().BeFalse();
        }
    }
}
