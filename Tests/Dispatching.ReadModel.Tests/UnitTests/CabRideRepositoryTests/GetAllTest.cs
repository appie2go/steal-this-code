using AutoFixture;
using Dispatching.ReadModel.PersistenceModel;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatching.ReadModel.Tests.UnitTests.CabRideRepositoryTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class GetAllTest
    {
        private readonly Fixture _fixture = new Fixture();

        private string _databaseName;

        private DispatchingReadDbContext _dbContext;
        private CabRideRepository _sut;

        [TestInitialize]
        public void Initialize()
        {
            _databaseName = _fixture.Create<string>();
        }

        [TestMethod]
        public async Task WhenCabRidesTableContainsRows_ShouldReturnCabRides()
        {
            // Arrange
            _dbContext = new DispatchingReadDbContextBuilder(_databaseName)
                .WithCabRide(_fixture.Create<CabRide>())
                .WithCabRide(_fixture.Create<CabRide>())
                .Build();

            // Act
            _sut = new CabRideRepository(_dbContext);
            var actual = await _sut.GetAll();

            // Assert
            actual.Count().Should().Be(_dbContext.CabRides.Count());
        }


        [TestMethod]
        public async Task WhenCabRideTableDoesNotContainRows_ShouldNotReturnCabRides()
        {
            // Arrange
            _dbContext = new DispatchingReadDbContextBuilder(_databaseName)
                .Build();

            // Act
            _sut = new CabRideRepository(_dbContext);
            var actual = await _sut.GetAll();

            // Assert
            actual.Should().BeEmpty();
        }
    }
}
