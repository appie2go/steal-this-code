using AutoFixture;
using Dispatching.Persistence.Tests;
using Dispatching.ReadModel.PersistenceModel;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatching.ReadModel.Tests.UnitTests.CabRideRepositoryTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class UpdateTest
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

            // Save a cabride in the database
            _cabRide = _fixture.Create<CabRide>();
            using var dbContext = new DispatchingReadDbContextBuilder(_databaseName)
                .WithCabRide(_cabRide)
                .Build();

            // Simulate another connection to that database
            _dbContext = new DispatchingReadDbContextBuilder(_databaseName)
                .Build();

            _cabRide.CustomerId = _fixture.Create<Guid>();
            _cabRide.TimeOfArrival = _fixture.Create<DateTime>();
            _sut = new CabRideRepository(_dbContext);
        }

        [TestMethod]
        public async Task WhenExistingEntity_ShouldNotInsertNewRecord()
        {
            // Act
            await _sut.Save(_cabRide);

            // Assert
            using var dbContext = new InMemoryDispatchingDbContext(_databaseName);
            dbContext.CabRides.Count().Should().Be(1);
        }

        [TestMethod]
        public async Task WhenExistingEntity_SaveCustomerId()
        {
            // Act
            await _sut.Save(_cabRide);

            // Assert
            using var dbContext = new InMemoryDispatchingDbContext(_databaseName);
            dbContext.CabRides.Select(x => x.CustomerId).First().Should().Be(_cabRide.CustomerId);
        }

        [TestMethod]
        public async Task WhenExistingEntity_SaveTimeOfArrival()
        {
            // Act
            await _sut.Save(_cabRide);

            // Assert
            using var dbContext = new InMemoryDispatchingDbContext(_databaseName);
            dbContext.CabRides.Select(x => x.TimeOfArrival).First().Should().Be(_cabRide.TimeOfArrival);
        }
    }
}
