using AutoFixture;
using Dispatching.Api.Controllers;
using Dispatching.Broker;
using Dispatching.Broker.Commands;
using Dispatching.ReadModel;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Threading.Tasks;

namespace Dispatching.Api.Tests.UnitTests.CabRideControllerTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class PostTest
    {
        private readonly Fixture _fixture = new Fixture();

        private ICabRideRepository _cabRideRepository;
        private IQueue _queue;

        private CabRideController _sut;

        [TestInitialize]
        public void Initialize()
        {
            _cabRideRepository = Substitute.For<ICabRideRepository>();
            _queue = Substitute.For<IQueue>();

            _sut = new CabRideController(_cabRideRepository, _queue);
        }

        [TestMethod]
        public async Task WhenCommand_ShouldEnqueueCommand()
        {
            // Arrange
            var command = _fixture.Create<DriveCustomerToTrainStation>();

            // Act
            await _sut.Post(command);

            // Assert
            await _queue
                .Received(1)
                .Enqueue(Arg.Is(command));
        }

        [TestMethod]
        public async Task WhenNoCommand_ShouldThrowArgumentNullException()
        {
            // Arrange
            DriveCustomerToTrainStation command = null;

            // Act
            Func<Task> act = async () => await _sut.Post(command);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }
    }
}
