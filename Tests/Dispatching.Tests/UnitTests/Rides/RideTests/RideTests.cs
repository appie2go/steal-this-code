using AutoFixture;
using Dispatching.Cabs;
using Dispatching.Customers;
using Dispatching.Framework;
using Dispatching.Rides;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Dispatching.Tests.UnitTests.Rides.RideTests
{
    [TestClass]
    public class RideTests
    {
        private readonly Fixture _fixture = new Fixture();

        private Id<Ride> _id;
        private Id<Customer> _customerId;
        private Id<Cab> _cabId;

        [TestInitialize]
        public void Initialize()
        {
            _id = _fixture.Create<Id<Ride>>();
            _customerId = _fixture.Create<Id<Customer>>();
            _cabId = _fixture.Create<Id<Cab>>();
        }

        [TestMethod]
        public void WhenIdProvided_ShouldSetId()
        {
            // Act
            var actual = new Ride(_id, _customerId, _cabId);

            // Assert
            actual.Id.Should().Be(_id);
        }

        [TestMethod]
        public void WhenCustomerId_ShouldSetCustomerId()
        {
            // Act
            var actual = new Ride(_id, _customerId, _cabId);

            // Assert
            actual.CustomerId.Should().Be(_customerId);
        }

        [TestMethod]
        public void WhenCabId_ShouldSetCabId()
        {
            // Act
            var actual = new Ride(_id, _customerId, _cabId);

            // Assert
            actual.CabId.Should().Be(_cabId);
        }

        [TestMethod]
        public void WhenNoIdProvided_ShouldThrowArgumentNullException()
        {
            // Act
            Action act = () => new Ride(null, _customerId, _cabId);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void WhenNoCustomerIdProvided_ShouldThrowArgumentNullException()
        {
            // Act
            Action act = () => new Ride(_id, null, _cabId);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void WhenNoCabIdProvided_ShouldThrowArgumentNullException()
        {
            // Act
            Action act = () => new Ride(_id, _customerId, null);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }
    }
}
