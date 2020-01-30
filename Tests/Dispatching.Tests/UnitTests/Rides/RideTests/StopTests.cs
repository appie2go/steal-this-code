using AutoFixture;
using Dispatching.Rides;
using Dispatching.TestFixtures.DomainObjects;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


namespace Dispatching.Tests.UnitTests.Rides.RideTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class StopTests
    {

        private readonly Fixture _fixture = new Fixture();

        private Ride _ride;

        private DateTime _startTime;
        private DateTime _endTime;

        [TestInitialize]
        public void Initialize()
        {
            _fixture.Customize(new LocationCustomization());

            _ride = _fixture.Create<Ride>();

            _startTime = _fixture.Create<DateTime>();
            _endTime = _startTime.AddMinutes(_fixture.Create<int>());

            _ride.SetDestination(_fixture.Create<Location>());
        }

        [TestMethod]
        public void WhenNotStarted_ShouldThrowApplicationException()
        {
            // Act
            Action act = () => _ride.Stop(_endTime);

            // Assert
            act.Should().Throw<ApplicationException>("Cannot stop a ride which hasn't started..");
        }

        [TestMethod]
        public void WhenStopsEarlierThanStarted_ShouldThrowApplicationException()
        {
            // Arrange
            _ride.Start(_startTime);

            // Act
            Action act = () => _ride.Stop(_startTime.AddMinutes(-1));

            // Assert
            act.Should().Throw<ApplicationException>("A ride cannot end earlier than it started.");
        }

        [TestMethod]
        public void WhenStopsSameTimeItStarted_ShouldSetStopTime()
        {
            // Arrange
            _ride.Start(_startTime);

            // Act
            _ride.Stop(_startTime);

            // Assert
            _ride.Stopped.Should().Be(_startTime);
        }

        [TestMethod]
        public void WhenStopsLaterThanStarted_ShouldSetStopTime()
        {
            // Arrange
            _ride.Start(_startTime);

            // Act
            _ride.Stop(_endTime);

            // Assert
            _ride.Stopped.Should().Be(_endTime);
        }
    }
}
