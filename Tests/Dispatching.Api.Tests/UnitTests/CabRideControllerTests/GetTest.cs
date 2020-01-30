using AutoFixture;
using Dispatching.Api.Controllers;
using Dispatching.Broker;
using Dispatching.ReadModel;
using Dispatching.ReadModel.PersistenceModel;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Threading.Tasks;

namespace Dispatching.Api.Tests.UnitTests.CabRideControllerTests
{
    [TestClass]
    public class GetTest
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
        public async Task WhenGetting_ShouldQueryRepositoryById()
        {
            // Arrange
            var id = _fixture.Create<Guid>();

            // Act
            await _sut.Get(id);

            // Assert
            await _cabRideRepository
                .Received(1)
                .FindById(Arg.Is(id));
        }

        [TestMethod]
        public async Task WhenGetting_ShouldReturnWhateverTheRepositoryReturns()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            var expected = _fixture.Create<CabRide>();
            _cabRideRepository
              .FindById(Arg.Any<Guid>())
              .Returns(expected);

            // Act
            var actual = await _sut.Get(id);

            // Assert
            actual.Should().NotBeNull();
        }
    }
}
