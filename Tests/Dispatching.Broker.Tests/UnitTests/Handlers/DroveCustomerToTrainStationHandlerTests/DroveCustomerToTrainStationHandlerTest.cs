using Dispatching.Broker.Events.Mappers.ToReadModel;
using Dispatching.Broker.Handlers;
using Dispatching.ReadModel;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;

namespace Dispatching.Broker.Tests.UnitTests.Handlers.DroveCustomerToTrainStationHandlerTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class DroveCustomerToTrainStationHandlerTest
    {
        private ICallback _callback;
        private ICabRideRepository _cabRideRepository;
        private ICabRideMapper _cabRideMapper;

        [TestInitialize]
        public void Initialize()
        {
            _callback = Substitute.For<ICallback>();
            _cabRideRepository = Substitute.For<ICabRideRepository>();
            _cabRideMapper = Substitute.For<ICabRideMapper>();
        }

        [TestMethod]
        public void WhenNoCallBack_ShouldThrowArgumentNullException()
        {
            // Arrange
            ICallback callBack = null;

            // Act
            Action act = () => new DroveCustomerToTrainStationHandler(callBack, _cabRideRepository, _cabRideMapper);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void WhenNoCabRideRepository_ShouldThrowArgumentNullException()
        {
            // Arrange
            ICabRideRepository cabRideRepository = null;

            // Act
            Action act = () => new DroveCustomerToTrainStationHandler(_callback, cabRideRepository, _cabRideMapper);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void WhenNoCabRideMapper_ShouldThrowArgumentNullException()
        {
            // Arrange
            ICabRideMapper cabRideMapper = null;

            // Act
            Action act = () => new DroveCustomerToTrainStationHandler(_callback, _cabRideRepository, cabRideMapper);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }
    }
}
