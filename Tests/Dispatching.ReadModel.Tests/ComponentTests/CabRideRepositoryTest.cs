using AutoFixture;
using Dispatching.ReadModel.Configuration;
using Dispatching.ReadModel.PersistenceModel;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatching.ReadModel.Tests.ComponentTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class CabRideRepositoryTest
    {
        private Fixture _fixture = new Fixture();

        private string _databaseName;
        private ICabRideRepository _sut;
        private DispatchingReadDbContext _dbContext;

        [TestInitialize]
        public void Initialize()
        {
            _databaseName = _fixture.Create<string>();
            _dbContext = new DispatchingReadDbContextBuilder(_databaseName)
                .WithCabRide(_fixture.Create<CabRide>())
                .WithCabRide(_fixture.Create<CabRide>())
                .Build();

            // Bootstrap
            var serviceProvider = new ServiceCollection()
                .UseDispatchingReadModel(_fixture.Create<string>())
                .AddTransient((s) => _dbContext)
                .BuildServiceProvider();

            _sut = serviceProvider.GetService<ICabRideRepository>();
        }

        [TestMethod]
        public async Task WhenData_ShouldReturnItems()
        {
            // Act
            var actual = await _sut.GetAll();

            // Assert
            actual.Count().Should().Be(_dbContext.CabRides.Count());
        }
    }
}
