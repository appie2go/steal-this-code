using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Dispatching.ReadModel.Tests.UnitTests.CabRideRepositoryTests
{
    [TestClass]
    public class CabRideRepositoryTest
    {
        [TestMethod]
        public void WhenNoDbContext_ShouldThrowArgumentNullException()
        {
            // Arrange
            DispatchingReadDbContext dbContext = null;

            // Act
            Action act = () => new CabRideRepository(dbContext);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }
    }
}
