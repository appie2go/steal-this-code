using AutoFixture;
using Dispatching.Broker.Configuration;
using Dispatching.Broker.Events;
using Dispatching.Broker.Handlers;
using Dispatching.ReadModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Rebus.Bus;
using System.Threading.Tasks;

namespace Dispatching.Broker.Tests.ComponentTests
{
    [TestClass]
    [TestCategory("Component/Integration")]
    public class DroveCustomerToTrainStationHandlerTest
    {
        private Fixture _fixture = new Fixture();

        private ICallback _callback;

        private DroveCustomerToTrainStationHandler _sut;

        [TestInitialize]
        public void Initialize()
        {
            _callback = Substitute.For<ICallback>();

            // Bootstrap
            var serviceProvider = new ServiceCollection()
                .UseDispatchingBroker()
                .AddTransient((s) => _callback)
                .AddTransient((s) => Substitute.For<IBus>())
                .AddTransient((s) => Substitute.For<ICabRideRepository>())
                .BuildServiceProvider();

            _sut = serviceProvider.GetService<DroveCustomerToTrainStationHandler>();
        }

        [TestMethod]
        public async Task WhenDroveCustomerToTrainStation_ShouldCallback()
        {
            // Arrange
            var @event = _fixture.Create<DroveCustomerToTrainStation>();

            // Act
            await _sut.Handle(@event);

            // Assert
            await _callback
                .Received(1)
                .CallBack(@event.CabRideId);
        }
    }
}
