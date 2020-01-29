using AutoFixture;
using Dispatching.Cabs;
using Dispatching.Persistence.Mappers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Linq;
using System.Threading.Tasks;

namespace Dispatching.Persistence.Tests.UnitTests.CabRepositoryTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class InsertTest
    {
        private readonly Fixture _fixture = new Fixture();

        private string _databaseName;

        private Cab _cab;
        private PersistenceModel.Cab _mappedCab;

        [TestInitialize]
        public async Task Initialize()
        {
            _databaseName = _fixture.Create<string>();

            // Arrange 
            _cab = _fixture.Create<Cab>();
            _mappedCab = _fixture.Create<PersistenceModel.Cab>();

            var persistenceModelMapper = Substitute.For<IMapToDomainModel<PersistenceModel.Cab, Cab>>();
            var domainModelMapper = CreateDomainModelMapper(_mappedCab);

            // Act
            using (var dbContext = CreateDbContext())
            {    
                var sut = new CabRepository(dbContext, domainModelMapper, persistenceModelMapper);
                await sut.Update(_cab);
            }
        }

        [TestMethod]
        public async Task WhenCabExists_ShouldUpdateCab()
        {
            // Assert
            using var context = CreateDbContext();
            context.Cabs.Count().Should().Be(1);
        }

        [TestMethod]
        public async Task WhenCabExists_ShouldUpdateId()
        {
            // Assert
            using var context = CreateDbContext();
            context.Cabs.Select(x => x.Id).FirstOrDefault().Should().Be(_mappedCab.Id);
        }

        [TestMethod]
        public async Task WhenCabExists_ShouldLongitude()
        {
            // Assert
            using var context = CreateDbContext();
            context.Cabs.Select(x => x.Longitude).FirstOrDefault().Should().Be(_mappedCab.Longitude);
        }

        [TestMethod]
        public async Task WhenCabExists_ShouldLatitude()
        {
            // Assert
            using var context = CreateDbContext();
            context.Cabs.Select(x => x.Latitude).FirstOrDefault().Should().Be(_mappedCab.Latitude);
        }

        private DispatchingDbContext CreateDbContext() 
        {
            return new DispatchingDbContextBuilder(_databaseName).Build();
        }

        private IMapToPersistenceModel<Cab, PersistenceModel.Cab> CreateDomainModelMapper(PersistenceModel.Cab cab)
        {
            var mapper = Substitute.For<IMapToPersistenceModel<Cab, PersistenceModel.Cab>>();
            mapper
                .CreateNew()
                .Returns(cab);

            return mapper;
        }
    }
}
