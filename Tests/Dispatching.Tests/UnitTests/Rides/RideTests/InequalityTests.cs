using AutoFixture;
using Dispatching.Cabs;
using Dispatching.Customers;
using Dispatching.Framework;
using Dispatching.Rides;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dispatching.Tests.UnitTests.Rides.RideTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class InequalityTests
    {
        private readonly Fixture _fixture = new Fixture();

        private Id<Ride> _rideId;
        private Id<Cab> _cabId;
        private Id<Customer> _customerId;

        [TestInitialize]
        public void Initialize()
        {
            _rideId = _fixture.Create<Id<Ride>>();
            _cabId = _fixture.Create<Id<Cab>>();
            _customerId = _fixture.Create<Id<Customer>>();
        }

        [TestMethod]
        public void WhenSame_ComparisonShouldBeFalse()
        {
            // Arrange
            var instance1 = new Ride(_rideId, _customerId, _cabId);
            var instance2 = new Ride(_rideId, _customerId, _cabId);

            // Act
            var actual = instance1 != instance2;

            // Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void WhenDifferentCustomer_ComparisonShouldBeFalse()
        {
            // Arrange
            var instance1 = new Ride(_rideId, _fixture.Create<Id<Customer>>(), _cabId);
            var instance2 = new Ride(_rideId, _customerId, _cabId);

            // Act
            var actual = instance1 != instance2;

            // Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void WhenDifferentCabId_ComparisonShouldBeFalse()
        {
            // Arrange
            var instance1 = new Ride(_rideId, _customerId, _fixture.Create<Id<Cab>>());
            var instance2 = new Ride(_rideId, _customerId, _cabId);

            // Act
            var actual = instance1 != instance2;

            // Assert
            actual.Should().BeFalse();
        }


        [TestMethod]
        public void WhenDifferentRideId_ComparisonShouldBeTrue()
        {
            // Arrange
            var instance1 = new Ride(_fixture.Create<Id<Ride>>(), _customerId, _cabId);
            var instance2 = new Ride(_rideId, _customerId, _cabId);

            // Act
            var actual = instance1 != instance2;

            // Assert
            actual.Should().BeTrue();
        }
    }
}
