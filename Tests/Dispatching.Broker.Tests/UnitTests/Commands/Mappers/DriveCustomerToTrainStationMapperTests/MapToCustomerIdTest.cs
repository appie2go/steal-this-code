using AutoFixture;
using Dispatching.Broker.Commands;
using Dispatching.Broker.Commands.Mappers;
using Dispatching.Customers;
using DomainDrivenDesign.DomainObjects;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dispatching.Broker.Tests.UnitTests.Commands.Mappers.DriveCustomerToTrainStationMapperTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class MapToCustomerIdTest
    {
        private Fixture _fixture = new Fixture();

        [TestMethod]
        public void WhenCustomerId_ShouldMapToId()
        {
            // Arrange
            var input = _fixture.Create<DriveCustomerToTrainStation>();

            // Act
            var sut = new DriveCustomerToTrainStationMapper();
            var actual = sut.MapToCustomerId(input);

            // Assert
            var expected = Id<Customer>.Create(input.CustomerId);
            actual.Should().Be(expected);
        }
    }
}
