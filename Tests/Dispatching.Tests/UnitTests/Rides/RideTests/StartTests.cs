using AutoFixture;
using Dispatching.Rides;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Dispatching.Tests.UnitTests.Rides.RideTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class StartTests
    {
        private readonly Fixture _fixture = new Fixture();

        private Ride _ride;

        private DateTime _startTime;
        private DateTime _endTime;
        private Location _destination;

        [TestInitialize]
        public void Initialize()
        {
            _ride = _fixture.Create<Ride>();

            _startTime = _fixture.Create<DateTime>();
            _endTime = _startTime.AddMinutes(_fixture.Create<int>());
            _destination = _fixture.Create<Location>();
        }

        [TestMethod]
        public void WhenDestinationUnknown_ShouldThrowApplicationException()
        {
            // Act
            Action act = () => _ride.Start(_startTime);

            // Assert
            act.Should().Throw<ApplicationException>("Can't start the ride if I don't know where to go! Provide a destination first!");
        }

        [TestMethod]
        public void WhenRideStopped_ShouldThrowApplicationException()
        {
            // Arrange
            _ride.SetDestination(_destination);
            _ride.Start(_startTime);
            _ride.Stop(_endTime);

            // Act
            Action act = () => _ride.Start(_startTime);

            // Assert
            act.Should().Throw<ApplicationException>("The ride cannot start after it has ended.");

        }

        [TestMethod]
        public void WhenRideStarted_ShouldSetStartTime()
        {
            // Arrange
            _ride.SetDestination(_destination);

            // Act
            _ride.Start(_startTime);
            var actual = _ride.Started;

            // Assert
            actual.Should().Be(_startTime);
        }
    }
}
