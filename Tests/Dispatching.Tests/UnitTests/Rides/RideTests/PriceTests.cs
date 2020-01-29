using AutoFixture;
using Dispatching.Rides;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Dispatching.Tests.UnitTests.Rides.RideTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class PriceTests
    {
        private readonly Fixture _fixture = new Fixture();

        private Ride _ride;
        
        [TestInitialize]
        public void Initialize()
        {
            _ride = _fixture.Create<Ride>();
            _ride.SetDestination(_fixture.Create<Location>());
        }

        [TestMethod]
        public void WhenRideNotStarted_PriceIsZero()
        {
            // Act
            var actual = _ride.Price;

            // Assert
            actual.Should().Be(Euro.None);
        }

        [TestMethod]
        public void WhenRideNotStopped_PriceIsZero()
        {
            // Arrange
            _ride.Start(_fixture.Create<DateTime>());

            // Act
            var actual = _ride.Price;

            // Assert
            actual.Should().Be(Euro.None);
        }

        [TestMethod]
        public void WhenRideStopped_PriceEqualsElapsedMinutesTimesThreeEuroAndFiftyCents()
        {
            // Arrange
            var elapsedMinutes = _fixture.Create<int>();
            var startTime = _fixture.Create<DateTime>();

            _ride.Start(startTime);
            _ride.Stop(startTime.AddMinutes(elapsedMinutes));

            // Act
            var actual = _ride.Price;

            // Assert
            var expected = Euro.FromDecimal(elapsedMinutes * 3.5m);
            actual.Should().Be(expected);
        }
    }
}
