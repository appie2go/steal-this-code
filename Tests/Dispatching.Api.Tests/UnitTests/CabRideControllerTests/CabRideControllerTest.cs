using Dispatching.Api.Controllers;
using Dispatching.Broker;
using Dispatching.ReadModel;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;

namespace Dispatching.Api.Tests.UnitTests.CabRideControllerTests
{
    [TestClass]
    public class CabRideControllerTest
    {
        private ICabRideRepository _cabRideRepository;
        private IQueue _queue;

        [TestInitialize]
        public void Initialize()
        {
            _cabRideRepository = Substitute.For<ICabRideRepository>();
            _queue = Substitute.For<IQueue>();
        }

        [TestMethod]
        public void WhenNoRepository_ShouldThrowArgumentNullException()
        {
            // Arrange
            ICabRideRepository cabRideRepository = null;

            // Act
            Action act = () => new CabRideController(cabRideRepository, _queue);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void WhenNoQueue_ShouldThrowArgumentNullException()
        {
            // Arrange
            IQueue queue = null;

            // Act
            Action act = () => new CabRideController(_cabRideRepository, queue);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }
    }
}
