using AutoFixture;
using Dispatching.Rides;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Dispatching.Tests.UnitTests.Rides.KilometerTests
{
    [TestClass]
    public class KilometerTests
    {
        private readonly Fixture _fixture = new Fixture();

        [TestMethod]
        public void WhenNegativeDistance_ShouldThrowArgumentException()
        {
            // Arrange
            var negativeDistance = -1 * _fixture.Create<decimal>();

            // Act
            Action act = () => Kilometer.FromDecimal(negativeDistance);

            // Asset
            act.Should().Throw<ArgumentException>("Distance must be bigger than 0 km.");
        }

        [TestMethod]
        public void WhenDistanceProvided_ShouldSetDistance()
        {
            // Arrange
            var distance = _fixture.Create<decimal>();

            // Act
            var actual = Kilometer.FromDecimal(distance);

            // Assert
            actual.ToDecimal().Should().Be(distance);
        }

        [TestMethod]
        public void WhenDistanceProvided_ShouldDisplayDistanceAsKilometers()
        {
            // Arrange
            var distance = _fixture.Create<decimal>();

            // Act
            var actual = Kilometer.FromDecimal(distance);

            // Assert
            actual.ToString().Should().Be($"{distance} km");
        }

        [TestMethod]
        public void WhenKilometersNone_ShouldEqualZeroKilomters()
        {
            // Act
            var actual = Kilometer.None;

            // Assert
            actual.Should().Be(Kilometer.FromDecimal(0));
        }
    }
}
