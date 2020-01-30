using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rebus.Bus;
using System;

namespace Dispatching.Broker.Tests.UnitTests.RebusQueueTests
{
    [TestClass]
    public class RebusQueueTest
    {
        [TestMethod]
        public void WhenNoBus_ShouldThrowArgumentNullException()
        {
            // Arrange
            IBus bus = null;

            // Act
            Action act = () => new RebusQueue(bus);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }
    }
}
