using AutoFixture;
using Dispatching.Customers;
using Dispatching.Framework;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dispatching.Tests.UnitTests.Customers.CustomerTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class EqualityTest
    {
        private readonly Fixture _fixture = new Fixture();

        private Id<Customer> _id;

        [TestInitialize]
        public void Initialize()
        {
            _id = _fixture.Create<Id<Customer>>();
        }

        [TestMethod]
        public void WhenSameId_ComparisonShouldBeTrue()
        {
            // Arrange
            var customer = new Customer(_id);
            var sameCustomer = new Customer(_id);

            // Act
            var actual = customer == sameCustomer;

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenDifferentIds_ComparisonShouldBeFalse()
        {
            // Arrange
            var customer = new Customer(_id);
            var otherCustomer = new Customer(_fixture.Create<Id<Customer>>());

            // Act
            var actual = customer == otherCustomer;

            // Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void WhenSameInstance_ComparisonShouldBeTrue()
        {
            // Arrange
            var customer = new Customer(_id);

            // Act
#pragma warning disable CS1718 // Comparison made to same variable
            var actual = customer == customer;
#pragma warning disable CS1718 // Comparison made to same variable

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenSameId_ShouldBeEqual()
        {
            // Arrange
            var customer = new Customer(_id);
            var sameCustomer = new Customer(_id);

            // Act
            var actual = customer.Equals(sameCustomer);

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenDifferentIds_ShouldNotBeEqual()
        {
            // Arrange
            var customer = new Customer(_id);
            var otherCustomer = new Customer(_fixture.Create<Id<Customer>>());

            // Act
            var actual = customer.Equals(otherCustomer);

            // Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void WhenSameInstance_ShouldBeEqual()
        {
            // Arrange
            var customer = new Customer(_id);

            // Act
            var actual = customer.Equals(customer);

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenCastedToObjectAndSameId_ShouldBeEqual()
        {
            // Arrange
            var customer = new Customer(_id);
            var sameCustomer = new Customer(_id);

            // Act
            var actual = customer.Equals((object)sameCustomer);

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenCastedToObjectAndDifferentIds_ShouldNotBeEqual()
        {
            // Arrange
            var customer = new Customer(_id);
            var otherCustomer = new Customer(_fixture.Create<Id<Customer>>());

            // Act
            var actual = customer.Equals((object)otherCustomer);

            // Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void WhenCastedToObjectAndSameInstance_ShouldBeEqual()
        {
            // Arrange
            var customer = new Customer(_id);

            // Act
            var actual = customer.Equals((object)customer);

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenDifferentObjects_ShouldHaveDifferentHashcodes()
        {
            // Arrange
            var customer = _fixture.Create<Customer>();
            var otherCustomer = _fixture.Create<Customer>();

            // Act
            var actual1 = customer.GetHashCode();
            var actual2 = otherCustomer.GetHashCode();

            // Asset
            actual1.Should().NotBe(actual2);
        }
    }
}
