using AutoFixture;
using Dispatching.Cabs;
using Dispatching.Customers;
using Dispatching.Framework;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Dispatching.Tests.UnitTests.Cabs.CabTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class EmbarcTest
    {
        private readonly Fixture _fixture = new Fixture();

        private Cab _cab;

        [TestInitialize]
        public void Initialize()
        {
            _cab = _fixture.Create<Cab>();
        }

        [TestMethod]
        public void WhenCabIsFull_ShouldThrowApplicationException()
        {
            // Arrange
            _cab.Embarc(_fixture.Create<Id<Customer>>());
            _cab.Embarc(_fixture.Create<Id<Customer>>());
            _cab.Embarc(_fixture.Create<Id<Customer>>());
            _cab.Embarc(_fixture.Create<Id<Customer>>());

            // Act
            Action act = () => _cab.Embarc(_fixture.Create<Id<Customer>>());

            // Assert
            act.Should().Throw<ApplicationException>("The cab is full.");
        }

        [TestMethod]
        public void WhenCustomerAlreadyInTheCab_ShouldThrowApplicationException()
        {
            // Arrange
            var customer = _fixture.Create<Id<Customer>>();
            _cab.Embarc(customer);

            // Act
            Action act = () => _cab.Embarc(customer);

            // Assert
            act.Should().Throw<ApplicationException>("This person is already in the cab.");
        }

        [TestMethod]
        public void WhenCabIsEmpty_ShouldAddCustomerId()
        {
            // Arrange
            var customer = _fixture.Create<Id<Customer>>();

            // Act
            _cab.Embarc(customer);

            // Assert
            _cab.Passengers.Any(x => x == customer).Should().BeTrue();
        }
    }
}
