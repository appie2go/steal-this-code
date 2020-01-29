using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Dispatching.Broker.Events.Mappers.ToDomainModel;
using AutoFixture;
using FluentAssertions;
using Dispatching.Rides;

namespace Dispatching.Broker.Tests.UnitTests.Events.ToDomainModel.CabRideMapperTests
{
    [TestClass]
    public class MapSuccessEventTest
    {
        private readonly Fixture _fixture = new Fixture();

        private CabRideMapper _sut;

        private Ride _ride;

        [TestInitialize]
        public void Initialize()
        {
            _sut = new CabRideMapper();

            _ride = _fixture.Create<Ride>();
        }

        [TestMethod]
        public void WhenNoRide_ShouldThrowArgumentNullException()
        {
            // Arrange
            Ride ride = null;

            // Act
            Action act = () => _sut.MapSuccessEvent(ride);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void WhenId_ShouldMapId()
        {
            // Act
            var actual = _sut.MapSuccessEvent(_ride);

            // Assert
            actual.CabRideId.Should().Be(_ride.Id.ToGuid());
        }

        [TestMethod]
        public void WhenCustomerId_ShouldMapCustomerId()
        {
            // Act
            var actual = _sut.MapSuccessEvent(_ride);

            // Assert
            actual.CustomerId.Should().Be(_ride.CustomerId.ToGuid());
        }

        [TestMethod]
        public void WhenNotStopped_ShouldArrivalTimeShouldBeMinValue()
        {
            // Act
            var actual = _sut.MapSuccessEvent(_ride);

            // Assert
            actual.ArrivalTime.Should().Be(DateTime.MinValue);
        }

        [TestMethod]
        public void WhenStopped_ShouldArrivalTimeShouldBeMinValue()
        {
            // Arrange
            var startDate = _fixture.Create<DateTime>();

            _ride.SetDestination(_fixture.Create<Location>());
            _ride.Start(startDate);
            _ride.Stop(startDate);

            // Act
            var actual = _sut.MapSuccessEvent(_ride);

            // Assert
            actual.ArrivalTime.Should().Be(_ride.Stopped.Value);
        }
    }
}
