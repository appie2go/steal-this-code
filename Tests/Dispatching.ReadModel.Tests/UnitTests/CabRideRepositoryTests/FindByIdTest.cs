using AutoFixture;
using Dispatching.ReadModel.Mappers;
using Dispatching.ReadModel.PersistenceModel;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Threading.Tasks;

namespace Dispatching.ReadModel.Tests.UnitTests.CabRideRepositoryTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class FindByIdTest
    {
        private readonly Fixture _fixture = new Fixture();

        private string _databaseName;

        private DispatchingReadDbContext _dbContext;
        private CabRideRepository _sut;

        [TestInitialize]
        public void Initialize()
        {
            _databaseName = _fixture.Create<string>();
            
            _dbContext = new DispatchingReadDbContextBuilder(_databaseName)
                .Build();

            _sut = new CabRideRepository(_dbContext, Substitute.For<IApply<CabRide>>());
        }

        [TestMethod]
        public async Task WhenItemExists_ShouldReturnItem()
        {
            // Arrange
            var cabRide = _fixture.Create<CabRide>();
            using var dbContext = new DispatchingReadDbContextBuilder(_databaseName)
                .WithCabRide(cabRide)
                .Build();

            // Act
            var actual = await _sut.FindById(cabRide.Id);

            // Assert
            actual.Id.Should().Be(cabRide.Id);
        }

        [TestMethod]
        public async Task WhenItemDoesNotExist_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var cabId = _fixture.Create<Guid>();

            // Act
            var actual = await _sut.FindById(cabId);

            // Assert
            actual.Should().BeNull();
        }
    }
}
