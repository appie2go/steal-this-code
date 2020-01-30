using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Rebus.Bus;
using System.Threading.Tasks;

namespace Dispatching.Broker.Tests.UnitTests.RebusQueueTests
{
    [TestClass]
    public class EnqueueTest
    {
        private readonly Fixture _fixture = new Fixture();

        private IBus _bus;

        private RebusQueue _sut;

        [TestInitialize]
        public void Initialize()
        {
            _bus = Substitute.For<IBus>();
            _sut = new RebusQueue(_bus);
        }

        [TestMethod]
        public async Task WhenMessage_ShouldForwardToRebus()
        {
            // Arrange
            var payload = _fixture.Create<object>();

            // Act
            await _sut.Enqueue(payload);

            // Assert
            await _bus
                .Received(1)
                .Publish(Arg.Is(payload));
        }
    }
}
