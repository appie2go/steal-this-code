using AutoFixture;
using Dispatching.Persistence.Tests;
using Dispatching.ReadModel.PersistenceModel;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatching.ReadModel.Tests.UnitTests.CabRideRepositoryTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class InsertTest
    {
        private readonly Fixture _fixture = new Fixture();

        private string _databaseName;

        private DispatchingReadDbContext _dbContext;
        private CabRideRepository _sut;
        private CabRide _cabRide;

        [TestInitialize]
        public void Initialize()
        {
            _databaseName = _fixture.Create<string>();

            _dbContext = new DispatchingReadDbContextBuilder(_databaseName)
                .Build();

            _sut = new CabRideRepository(_dbContext);
            _cabRide = _fixture.Create<CabRide>();
        }

        [TestMethod]
        public async Task WhenNewEntity_ShouldInsertOneNewRecord()
        {
            // Act
            await _sut.Save(_cabRide);

            // Assert
            using var dbContext = new InMemoryDispatchingDbContext(_databaseName);
            dbContext.CabRides.Count().Should().Be(1);
        }

        [TestMethod]
        public async Task WhenNewEntity_SaveCustomerId()
        {
            // Act
            await _sut.Save(_cabRide);

            // Assert
            using var dbContext = new InMemoryDispatchingDbContext(_databaseName);
            dbContext.CabRides.Select(x => x.CustomerId).First().Should().Be(_cabRide.CustomerId);
        }

        [TestMethod]
        public async Task WhenNewEntity_SaveId()
        {
            // Act
            await _sut.Save(_cabRide);

            // Assert
            using var dbContext = new InMemoryDispatchingDbContext(_databaseName);
            dbContext.CabRides.Select(x => x.Id).First().Should().Be(_cabRide.Id);
        }

        [TestMethod]
        public async Task WhenNewEntity_SaveTimeOfArrival()
        {
            // Act
            await _sut.Save(_cabRide);

            // Assert
            using var dbContext = new InMemoryDispatchingDbContext(_databaseName);
            dbContext.CabRides.Select(x => x.TimeOfArrival).First().Should().Be(_cabRide.TimeOfArrival);
        }
    }
}
