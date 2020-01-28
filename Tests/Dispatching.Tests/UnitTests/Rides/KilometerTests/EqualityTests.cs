using AutoFixture;
using Dispatching.Rides;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dispatching.Tests.UnitTests.Rides.KilometerTests
{
    [TestClass]
    public class EqualityTests
    {
        private readonly Fixture _fixture = new Fixture();

        private decimal _distance;

        [TestInitialize]
        public void Initialize()
        {
            _distance = _fixture.Create<decimal>();
        }

        [TestMethod]
        public void WhenSameDistance_ComparisonShouldBeTrue()
        {
            // Arrange
            var instance1 = Kilometer.FromDecimal(_distance);
            var instance2 = Kilometer.FromDecimal(_distance);

            // Act
            var actual = instance1 == instance2;

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenDifferentDistance_ComparisonShouldBeFalse()
        {
            // Arrange
            var instance1 = Kilometer.FromDecimal(_distance);
            var instance2 = Kilometer.FromDecimal(_fixture.Create<decimal>());

            // Act
            var actual = instance1 == instance2;

            // Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void WhenSameInstance_ComparisonShouldBeTrue()
        {
            // Arrange
            var instance = Kilometer.FromDecimal(_distance);

            // Act
            var actual = instance == instance;

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenSameDistance_ShouldBeEqual()
        {
            // Arrange
            var instance1 = Kilometer.FromDecimal(_distance);
            var instance2 = Kilometer.FromDecimal(_distance);

            // Act
            var actual = instance1.Equals(instance2);

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenDifferentDistance_ShouldNotBeEqual()
        {
            // Arrange
            var instance1 = Kilometer.FromDecimal(_distance);
            var instance2 = Kilometer.FromDecimal(_fixture.Create<decimal>());

            // Act
            var actual = instance1.Equals(instance2);

            // Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void WhenSameInstance_ShouldBeEqual()
        {
            // Arrange
            var instance = Kilometer.FromDecimal(_distance);

            // Act
            var actual = instance.Equals(instance);

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenCastedToObjectAndSameDistance_ShouldBeEqual()
        {
            // Arrange
            var instance1 = Kilometer.FromDecimal(_distance);
            var instance2 = Kilometer.FromDecimal(_distance);

            // Act
            var actual = instance1.Equals((object)instance2);

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenCastedToObjectAndDifferentDistance_ShouldNotBeEqual()
        {
            // Arrange
            var instance1 = Kilometer.FromDecimal(_distance);
            var instance2 = Kilometer.FromDecimal(_fixture.Create<decimal>());

            // Act
            var actual = instance1.Equals((object)instance2);

            // Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void WhenCastedToObjectAndSameInstance_ShouldBeEqual()
        {
            // Arrange
            var instance = Kilometer.FromDecimal(_distance);

            // Act
            var actual = instance.Equals((object)instance);

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenDifferentObjects_ShouldHaveDifferentHashcodes()
        {
            // Arrange
            var distance = Kilometer.FromDecimal(_fixture.Create<decimal>());
            var otherDistance = Kilometer.FromDecimal(_fixture.Create<decimal>());

            // Act
            var actual1 = distance.GetHashCode();
            var actual2 = otherDistance.GetHashCode();

            // Asset
            actual1.Should().NotBe(actual2);
        }
    }
}
