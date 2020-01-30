using AutoFixture;
using Dispatching.Broker.Commands;
using Dispatching.Broker.Configuration;
using Dispatching.Broker.Events;
using Dispatching.Broker.Handlers;
using Dispatching.Customers;
using Dispatching.Framework;
using Dispatching.Rides;
using Dispatching.Rides.Processes.PrimaryPorts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Rebus.Bus;
using System.Threading.Tasks;

namespace Dispatching.Broker.Tests.ComponentTests
{
    [TestClass]
    public class DriveCustomerToTrainStationHandlerTest
    {
        private Fixture _fixture = new Fixture();

        private IBus _bus;
        private ICabRideService _cabRideService;

        private DriveCustomerToTrainStationHandler _sut;

        [TestInitialize]
        public void Initialize()
        {
            _bus = Substitute.For<IBus>();

            _cabRideService = Substitute.For<ICabRideService>();
            _cabRideService
                .BringCustomerToTheTrainStation(Arg.Any<Id<Customer>>(), Arg.Any<Location>())
                .Returns(_fixture.Create<Ride>());

            // Bootstrap
            var serviceProvider = new ServiceCollection()
                .UseDispatchingBroker()
                .AddTransient((s) => _bus)
                .AddTransient((s) => _cabRideService)
                .BuildServiceProvider();

            _sut = serviceProvider.GetService<DriveCustomerToTrainStationHandler>();
        }

        [TestMethod]
        public async Task WhenDriveCustomerToTrainStation_ShouldDroveCustomerToTrainStation()
        {
            // Arrange
            var message = _fixture.Create<DriveCustomerToTrainStation>();

            // Act
            await _sut.Handle(message);

            // Assert
            await _bus
                .Received(1)
                .Publish(Arg.Any<DroveCustomerToTrainStation>());
        }
    }
}
