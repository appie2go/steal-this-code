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
    public class EqualityTests
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
        public void WhenSame_ComparisonShouldBeTrue()
        {
            // Arrange
            var instance1 = new Ride(_rideId, _customerId, _cabId);
            var instance2 = new Ride(_rideId, _customerId, _cabId);

            // Act
            var actual = instance1 == instance2;

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenDifferentCustomer_ComparisonShouldBeTrue()
        {
            // Arrange
            var instance1 = new Ride(_rideId, _fixture.Create<Id<Customer>>(), _cabId);
            var instance2 = new Ride(_rideId, _customerId, _cabId);

            // Act
            var actual = instance1 == instance2;

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenDifferentCabId_ComparisonShouldBeTrue()
        {
            // Arrange
            var instance1 = new Ride(_rideId, _customerId, _fixture.Create<Id<Cab>>());
            var instance2 = new Ride(_rideId, _customerId, _cabId);

            // Act
            var actual = instance1 == instance2;

            // Assert
            actual.Should().BeTrue();
        }


        [TestMethod]
        public void WhenDifferentRideId_ComparisonShouldBeFalse()
        {
            // Arrange
            var instance1 = new Ride(_fixture.Create<Id<Ride>>(), _customerId, _cabId);
            var instance2 = new Ride(_rideId, _customerId, _cabId);

            // Act
            var actual = instance1 == instance2;

            // Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void WhenSame_ShouldBeEqual()
        {
            // Arrange
            var instance1 = new Ride(_rideId, _customerId, _cabId);
            var instance2 = new Ride(_rideId, _customerId, _cabId);

            // Act
            var actual = instance1.Equals(instance2);

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenDifferentCustomer_ShouldBeEqual()
        {
            // Arrange
            var instance1 = new Ride(_rideId, _fixture.Create<Id<Customer>>(), _cabId);
            var instance2 = new Ride(_rideId, _customerId, _cabId);

            // Act
            var actual = instance1.Equals(instance2);

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenDifferentCabId_ShouldBeEqual()
        {
            // Arrange
            var instance1 = new Ride(_rideId, _customerId, _fixture.Create<Id<Cab>>());
            var instance2 = new Ride(_rideId, _customerId, _cabId);

            // Act
            var actual = instance1.Equals(instance2);

            // Assert
            actual.Should().BeTrue();
        }


        [TestMethod]
        public void WhenDifferentRideId_ShouldNotBeEqual()
        {
            // Arrange
            var instance1 = new Ride(_fixture.Create<Id<Ride>>(), _customerId, _cabId);
            var instance2 = new Ride(_rideId, _customerId, _cabId);

            // Act
            var actual = instance1.Equals(instance2);

            // Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void WhenCastedToObjectAndSame_ShouldBeEqual()
        {
            // Arrange
            var instance1 = new Ride(_rideId, _customerId, _cabId);
            var instance2 = new Ride(_rideId, _customerId, _cabId);

            // Act
            var actual = instance1.Equals((object)instance2);

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenCastedToObjectAndDifferentCustomer_ShouldBeEqual()
        {
            // Arrange
            var instance1 = new Ride(_rideId, _fixture.Create<Id<Customer>>(), _cabId);
            var instance2 = new Ride(_rideId, _customerId, _cabId);

            // Act
            var actual = instance1.Equals((object)instance2);

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenCastedToObjectAndDifferentCabId_ShouldBeEqual()
        {
            // Arrange
            var instance1 = new Ride(_rideId, _customerId, _fixture.Create<Id<Cab>>());
            var instance2 = new Ride(_rideId, _customerId, _cabId);

            // Act
            var actual = instance1.Equals((object)instance2);

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenCastedToObjectAndDifferentRideId_ShouldNotBeEqual()
        {
            // Arrange
            var instance1 = new Ride(_fixture.Create<Id<Ride>>(), _customerId, _cabId);
            var instance2 = new Ride(_rideId, _customerId, _cabId);

            // Act
            var actual = instance1.Equals((object)instance2);

            // Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void WhenDifferentObjects_ShouldHaveDifferentHashcodes()
        {
            // Arrange
            var ride = _fixture.Create<Ride>();
            var otherRide = _fixture.Create<Ride>();

            // Act
            var actual1 = ride.GetHashCode();
            var actual2 = otherRide.GetHashCode();

            // Asset
            actual1.Should().NotBe(actual2);
        }
    }
}
