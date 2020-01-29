using AutoFixture;
using Dispatching.Broker.Events;
using Dispatching.Broker.Events.Mappers.ToReadModel;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Dispatching.Broker.Tests.UnitTests.Events.ToReadModel.CabRideMapperTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class MapTest
    {
        private readonly Fixture _fixture = new Fixture();

        private CabRideMapper _sut;

        private DroveCustomerToTrainStation _event;

        [TestInitialize]
        public void Initialize()
        {
            _sut = new CabRideMapper();

            _event = _fixture.Create<DroveCustomerToTrainStation>();
        }

        [TestMethod]
        public void WhenEventNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            DroveCustomerToTrainStation @event = null;

            // Act
            Action act = () => _sut.Map(@event);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void WhenCabRideId_ShouldMapToId()
        {
            // Act
            var actual = _sut.Map(_event);

            // Assert
            actual.Id.Should().Be(_event.CabRideId);
        }

        [TestMethod]
        public void WhenCustomerId_ShouldMapToCustomerId()
        {
            // Act
            var actual = _sut.Map(_event);

            // Assert
            actual.CustomerId.Should().Be(_event.CustomerId);
        }

        [TestMethod]
        public void WhenArrivalTime_ShouldMapToTimeOfArrival()
        {
            // Act
            var actual = _sut.Map(_event);

            // Assert
            actual.TimeOfArrival.Should().Be(_event.ArrivalTime);
        }
    }
}
