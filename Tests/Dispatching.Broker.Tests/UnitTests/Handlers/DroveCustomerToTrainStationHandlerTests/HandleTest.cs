using AutoFixture;
using Dispatching.Broker.Events;
using Dispatching.Broker.Events.Mappers.ToReadModel;
using Dispatching.Broker.Handlers;
using Dispatching.ReadModel;
using Dispatching.ReadModel.PersistenceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Threading.Tasks;

namespace Dispatching.Broker.Tests.UnitTests.Handlers.DroveCustomerToTrainStationHandlerTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class HandleTest
    {
        private readonly Fixture _fixture = new Fixture();

        private ICallback _callback;
        private ICabRideRepository _cabRideRepository;
        private ICabRideMapper _cabRideMapper;

        private DroveCustomerToTrainStationHandler _sut;

        [TestInitialize]
        public void Initialize()
        {
            _callback = Substitute.For<ICallback>();
            _cabRideRepository = Substitute.For<ICabRideRepository>();
            _cabRideMapper = Substitute.For<ICabRideMapper>();

            _sut = new DroveCustomerToTrainStationHandler(_callback, _cabRideRepository, _cabRideMapper);
        }

        [TestMethod]
        public async Task WhenDroveCustomerToTrainStation_ShouldInvokeCallback()
        {
            // Arrange
            var payload = _fixture.Create<DroveCustomerToTrainStation>();

            // Act
            await _sut.Handle(payload);

            // Assert
            await _callback
                .Received(1)
                .CallBack(Arg.Is(payload.CabRideId));
        }

        [TestMethod]
        public async Task WhenDroveCustomerToTrainStation_ShouldMapToReadModel()
        {
            // Arrange
            var payload = _fixture.Create<DroveCustomerToTrainStation>();

            // Act
            await _sut.Handle(payload);

            // Assert
            _cabRideMapper
                .Received(1)
                .Map(Arg.Is(payload));
        }

        [TestMethod]
        public async Task WhenDroveCustomerToTrainStation_ShouldUpdateReadModel()
        {
            // Arrange
            var payload = _fixture.Create<DroveCustomerToTrainStation>();
            var expected = _fixture.Create<CabRide>();

            _cabRideMapper
                .Map(Arg.Any<DroveCustomerToTrainStation>())
                .Returns(expected);

            // Act
            await _sut.Handle(payload);

            // Assert
            await _cabRideRepository
                .Received(1)
                .Save(Arg.Is(expected));
        }
    }
}
