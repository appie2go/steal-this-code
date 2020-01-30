using AutoFixture;
using Dispatching.ReadModel.Mappers;
using Dispatching.ReadModel.PersistenceModel;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;

namespace Dispatching.ReadModel.Tests.UnitTests.CabRideRepositoryTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class GetAllTest
    {
        private readonly Fixture _fixture = new Fixture();

        private string _databaseName;

        private DispatchingReadDbContext _dbContext;
        private IApply<CabRide> _mapper;
        
        [TestInitialize]
        public void Initialize()
        {
            _databaseName = _fixture.Create<string>();
            _mapper = Substitute.For<IApply<CabRide>>();
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
            var sut = new CabRideRepository(_dbContext, _mapper);
            var actual = await sut.GetAll();

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
            var sut = new CabRideRepository(_dbContext, _mapper);
            var actual = await sut.GetAll();

            // Assert
            actual.Should().BeEmpty();
        }
    }
}
