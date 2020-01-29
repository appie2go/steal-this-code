using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Dispatching.Broker.Events.Mappers.ToDomainModel;
using AutoFixture;
using Dispatching.Broker.Commands;
using FluentAssertions;

namespace Dispatching.Broker.Tests.UnitTests.Events.ToDomainModel.CabRideMapperTests
{
    [TestClass]
    public class MapFailedEventTest
    {
        private readonly Fixture _fixture = new Fixture();

        private CabRideMapper _sut;

        private DriveCustomerToTrainStation _command;
        private Exception _ex;

        [TestInitialize]
        public void Initialize()
        {
            _sut = new CabRideMapper();

            _command = _fixture.Create<DriveCustomerToTrainStation>();
            _ex = _fixture.Create<Exception>();
        }

        [TestMethod]
        public void WhenException_ShouldPopulateReasonField()
        {
            // Act
            var actual = _sut.MapFailedEvent(_command, _ex);

            // Assert
            actual.Reason.Should().Be(_ex.ToString());
        }

        [TestMethod]
        public void WhenNoCommand_ShouldThrowArgumentNullException()
        {
            // Arrange
            DriveCustomerToTrainStation command = null;

            // Act
            Action act = () => _sut.MapFailedEvent(command, _ex);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void WhenNoException_ShouldThrowArgumentNullException()
        {
            // Arrange
            Exception ex = null;

            // Act
            Action act = () => _sut.MapFailedEvent(_command, ex);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }
    }
}
