using AutoFixture;
using Dispatching.Broker.Commands;
using Dispatching.Broker.Commands.Mappers;
using Dispatching.Broker.Events;
using Dispatching.Broker.Events.Mappers.ToDomainModel;
using Dispatching.Broker.Handlers;
using Dispatching.Customers;
using Dispatching.Rides;
using Dispatching.Rides.Processes.PrimaryPorts;
using DomainDrivenDesign.DomainObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Threading.Tasks;

namespace Dispatching.Broker.Tests.UnitTests.Handlers.DriveCustomerToTrainStationHandlerTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class HandleTest
    {
        private readonly Fixture _fixture = new Fixture();

        private IQueue _messageBus;
        private ICabRideService _cabRideService;
        private IDriveCustomerToTrainStationMapper _driveCustomerToTrainStationMapper;
        private ICabRideMapper _cabRideMapper;

        private DriveCustomerToTrainStationHandler _sut;

        [TestInitialize]
        public void Initialize()
        {
            _messageBus = Substitute.For<IQueue>();
            _cabRideService = Substitute.For<ICabRideService>();
            _driveCustomerToTrainStationMapper = Substitute.For<IDriveCustomerToTrainStationMapper>();
            _cabRideMapper = Substitute.For<ICabRideMapper>();

            _driveCustomerToTrainStationMapper
                .MapToCustomerId(Arg.Any<DriveCustomerToTrainStation>())
                .Returns(_fixture.Create<Id<Customer>>());

            _driveCustomerToTrainStationMapper
                .MapToCustomerLocation(Arg.Any<DriveCustomerToTrainStation>())
                .Returns(_fixture.Create<Location>());

            _cabRideMapper
                .MapFailedEvent(Arg.Any<DriveCustomerToTrainStation>(), Arg.Any<Exception>())
                .Returns(_fixture.Create<DriveCustomerToTrainStationFailed>());

            _cabRideMapper
                .MapSuccessEvent(Arg.Any<Ride>())
                .Returns(_fixture.Create<DroveCustomerToTrainStation>());

            _sut = new DriveCustomerToTrainStationHandler(_messageBus, _cabRideService, _driveCustomerToTrainStationMapper, _cabRideMapper);
        }

        [TestMethod]
        public async Task WhenSuccess_ShouldRaiseDroveCustomerToTrainStationEvent()
        {
            // Arrange
            var command = _fixture.Create<DriveCustomerToTrainStation>();

            // Act
            await _sut.Handle(command);

            // Assert
            await _messageBus
                .Received(1)
                .Enqueue(Arg.Any<DroveCustomerToTrainStation>());
        }

        [TestMethod]
        public async Task WhenFailiure_ShouldRaiseDriveCustomerToTrainStationFailedEvent()
        {
            // Arrange
            var command = _fixture.Create<DriveCustomerToTrainStation>();

            _cabRideService
                .When(x => x.BringCustomerToTheTrainStation(Arg.Any<Id<Customer>>(), Arg.Any<Location>()))
                .Do((x) => throw new Exception());

            // Act
            await _sut.Handle(command);

            // Assert
            await _messageBus
                .Received(1)
                .Enqueue(Arg.Any<DriveCustomerToTrainStationFailed>());
        }

        [TestMethod]
        public async Task WhenCustomerId_ShouldMapCustomerIdIntoDomainEntity()
        {
            // Arrange
            var command = _fixture.Create<DriveCustomerToTrainStation>();

            // Act
            await _sut.Handle(command);

            // Assert
            _driveCustomerToTrainStationMapper
                .Received(1)
                .MapToCustomerId(Arg.Is(command));
        }

        [TestMethod]
        public async Task WhenLocation_ShouldMapLocationIntoDomainEntity()
        {
            // Arrange
            var command = _fixture.Create<DriveCustomerToTrainStation>();

            // Act
            await _sut.Handle(command);

            // Assert
            _driveCustomerToTrainStationMapper
                .Received(1)
                .MapToCustomerLocation(Arg.Is(command));
        }

        [TestMethod]
        public async Task WhenCommand_ShouldInvokeDomainServiceWithResultsFromMapper()
        {
            // Arrange
            var command = _fixture.Create<DriveCustomerToTrainStation>();

            var expectedCustomerId = _fixture.Create<Id<Customer>>();
            _driveCustomerToTrainStationMapper
             .MapToCustomerId(Arg.Any<DriveCustomerToTrainStation>())
             .Returns(expectedCustomerId);

            var expectedLocation = _fixture.Create<Location>();
            _driveCustomerToTrainStationMapper
                .MapToCustomerLocation(Arg.Any<DriveCustomerToTrainStation>())
                .Returns(expectedLocation);

            // Act
            await _sut.Handle(command);

            // Assert
            await _cabRideService
                .Received(1)
                .BringCustomerToTheTrainStation(Arg.Is(expectedCustomerId), Arg.Is(expectedLocation));
        }
    }
}
