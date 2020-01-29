using Dispatching.Broker.Commands.Mappers;
using Dispatching.Broker.Events.Mappers.ToDomainModel;
using Dispatching.Broker.Handlers;
using Dispatching.Rides.Processes.PrimaryPorts;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;

namespace Dispatching.Broker.Tests.UnitTests.Handlers.DriveCustomerToTrainStationHandlerTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class DriveCustomerToTrainStationHandlerTest
    {
        private IQueue _messageBus;
        private ICabRideService _cabRideService;
        private IDriveCustomerToTrainStationMapper _driveCustomerToTrainStationMapper;
        private ICabRideMapper _cabRideMapper;

        [TestInitialize]
        public void Initialize()
        {
            _messageBus = Substitute.For<IQueue>();
            _cabRideService = Substitute.For<ICabRideService>();
            _driveCustomerToTrainStationMapper = Substitute.For<IDriveCustomerToTrainStationMapper>();
            _cabRideMapper = Substitute.For<ICabRideMapper>();
        }

        [TestMethod]
        public void WhenNoMessageBus_ShouldThrowArgumentNullException()
        {
            // Arrange
            IQueue messageBus = null;

            // Act
            Action act = () => new DriveCustomerToTrainStationHandler(messageBus, _cabRideService, _driveCustomerToTrainStationMapper, _cabRideMapper);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void WhenNoCabRideService_ShouldThrowArgumentNullException()
        {
            // Arrange
            ICabRideService cabRideService = null;

            // Act
            Action act = () => new DriveCustomerToTrainStationHandler(_messageBus, cabRideService, _driveCustomerToTrainStationMapper, _cabRideMapper);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void WhenNoDriveCustomerToTrainStationMapper_ShouldThrowArgumentNullException()
        {
            // Arrange
            IDriveCustomerToTrainStationMapper driveCustomerToTrainStationMapper = null;

            // Act
            Action act = () => new DriveCustomerToTrainStationHandler(_messageBus, _cabRideService, driveCustomerToTrainStationMapper, _cabRideMapper);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void WhenNoCabRideMapper_ShouldThrowArgumentNullException()
        {
            // Arrange
            ICabRideMapper cabRideMapper = null;

            // Act
            Action act = () => new DriveCustomerToTrainStationHandler(_messageBus, _cabRideService, _driveCustomerToTrainStationMapper, cabRideMapper);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }
    }
}
