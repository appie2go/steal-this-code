using AutoFixture;
using Dispatching.Persistence.Tests;
using Dispatching.ReadModel.Mappers;
using Dispatching.ReadModel.PersistenceModel;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
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

        private IApply<CabRide> _mapper;

        [TestInitialize]
        public void Initialize()
        {
            _databaseName = _fixture.Create<string>();

            _dbContext = new DispatchingReadDbContextBuilder(_databaseName)
                .Build();

            _cabRide = _fixture.Create<CabRide>();
            _mapper = Substitute.For<IApply<CabRide>>();

            _sut = new CabRideRepository(_dbContext, _mapper);
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
        public async Task WhenNewEntity_ShouldApplyValues()
        {
            // Act
            await _sut.Save(_cabRide);

            // Assert
            _mapper
                .Received(1)
                .Apply(Arg.Any<CabRide>(), Arg.Is(_cabRide));
        }
    }
}
