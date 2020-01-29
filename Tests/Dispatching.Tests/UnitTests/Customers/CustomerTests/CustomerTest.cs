using AutoFixture;
using Dispatching.Customers;
using Dispatching.Framework;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dispatching.Tests.UnitTests.Customers.CustomerTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class CustomerTest
    {
        private Fixture _fixture = new Fixture();

        [TestMethod]
        public void WhenIdProvided_ShouldSetId()
        {
            // Arrange
            var id = _fixture.Create<Id<Customer>>();

            // Act
            var actual = new Customer(id);

            // Assert
            actual.Id.Should().Be(id);
        }
    }
}
