using AutoFixture;
using Dispatching.ReadModel.Mappers;
using Dispatching.ReadModel.PersistenceModel;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dispatching.ReadModel.Tests.UnitTests.Mappers.CabRideMapperTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class MapTest
    {
        private Fixture _fixture = new Fixture();

        private CabRideMapper _mapper;

        private CabRide _ride;

        [TestInitialize]
        public void Initialize()
        {
            _mapper = new CabRideMapper();
            _ride = _fixture.Create<CabRide>();
        }

        [TestMethod]
        public void WhenId_ShouldMapId()
        {
            // Arrange
            var actual = new CabRide();

            // Act
            _mapper.Apply(actual, _ride);

            // Assert
            actual.Id.Should().Be(_ride.Id);
        }

        [TestMethod]
        public void WhenCustomerId_ShouldMapCustomerId()
        {
            // Arrange
            var actual = new CabRide();

            // Act
            _mapper.Apply(actual, _ride);

            // Assert
            actual.CustomerId.Should().Be(_ride.CustomerId);
        }

        [TestMethod]
        public void WhenTimeOfArrival_ShouldMapTimeOfArrival()
        {
            // Arrange
            var actual = new CabRide();

            // Act
            _mapper.Apply(actual, _ride);

            // Assert
            actual.TimeOfArrival.Should().Be(_ride.TimeOfArrival);
        }
    }
}
