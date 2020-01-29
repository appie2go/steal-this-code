using AutoFixture;
using Dispatching.Rides;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dispatching.Tests.UnitTests.Rides.EuroTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class EqualityTests
    {
        private readonly Fixture _fixture = new Fixture();

        private decimal _amount;

        [TestInitialize]
        public void Initialize()
        {
            _amount = _fixture.Create<decimal>();
        }

        [TestMethod]
        public void WhenSameAmount_ComparisonShouldBeTrue()
        {
            // Arrange
            var instance1 = Euro.FromDecimal(_amount);
            var instance2 = Euro.FromDecimal(_amount);

            // Act
            var actual = instance1 == instance2;

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenDifferentAmount_ComparisonShouldBeFalse()
        {
            // Arrange
            var instance1 = Euro.FromDecimal(_amount);
            var instance2 = Euro.FromDecimal(_fixture.Create<decimal>());

            // Act
            var actual = instance1 == instance2;

            // Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void WhenSameInstance_ComparisonShouldBeTrue()
        {
            // Arrange
            var instance = Euro.FromDecimal(_amount);

            // Act
            var actual = instance == instance;

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenSameAmount_ShouldBeEqual()
        {
            // Arrange
            var instance1 = Euro.FromDecimal(_amount);
            var instance2 = Euro.FromDecimal(_amount);

            // Act
            var actual = instance1.Equals(instance2);

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenDifferentAmount_ShouldNotBeEqual()
        {
            // Arrange
            var instance1 = Euro.FromDecimal(_amount);
            var instance2 = Euro.FromDecimal(_fixture.Create<decimal>());

            // Act
            var actual = instance1.Equals(instance2);

            // Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void WhenSameInstance_ShouldBeEqual()
        {
            // Arrange
            var instance = Euro.FromDecimal(_amount);

            // Act
            var actual = instance.Equals(instance);

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenCastedToObjectAndSameAmount_ShouldBeEqual()
        {
            // Arrange
            var instance1 = Euro.FromDecimal(_amount);
            var instance2 = Euro.FromDecimal(_amount);

            // Act
            var actual = instance1.Equals((object)instance2);

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenCastedToObjectAndDifferentAmount_ShouldNotBeEqual()
        {
            // Arrange
            var instance1 = Euro.FromDecimal(_amount);
            var instance2 = Euro.FromDecimal(_fixture.Create<decimal>());

            // Act
            var actual = instance1.Equals((object)instance2);

            // Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void WhenCastedToObjectAndSameInstance_ShouldBeEqual()
        {
            // Arrange
            var instance = Euro.FromDecimal(_amount);

            // Act
            var actual = instance.Equals((object)instance);

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenDifferentObjects_ShouldHaveDifferentHashcodes()
        {
            // Arrange
            var amount = Euro.FromDecimal(_fixture.Create<decimal>());
            var otherAmount = Euro.FromDecimal(_fixture.Create<decimal>());

            // Act
            var actual1 = amount.GetHashCode();
            var actual2 = otherAmount.GetHashCode();

            // Asset
            actual1.Should().NotBe(actual2);
        }
    }
}
