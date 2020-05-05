using AutoFixture;
using Dispatching.Cabs;
using Dispatching.Persistence.Mappers;
using DomainDrivenDesign.DomainObjects;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dispatching.Persistence.Tests.UnitTests.CabRepositoryTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class GetTest
    {
        private readonly Fixture _fixture = new Fixture();

        private IMapToPersistenceModel<Cab, PersistenceModel.Cab> _domainModelMapper;
        private IMapToDomainModel<PersistenceModel.Cab, Cab> _persistenceModelMapper;


        [TestInitialize]
        public void Initialize()
        {
            _domainModelMapper = Substitute.For<IMapToPersistenceModel<Cab, PersistenceModel.Cab>>();
            _persistenceModelMapper = Substitute.For<IMapToDomainModel<PersistenceModel.Cab, Cab>>();
        }

        [TestMethod]
        public async Task WhenItemExists_ShouldMapEntityIntoDomainModel()
        {
            // Arrange
            var cab = _fixture.Create<PersistenceModel.Cab>();
            var cabId = new Id<Cab>(cab.Id);
            var dbContext = new DispatchingDbContextBuilder()
                .WithCab(cab)
                .Build();

            // Act
            using (dbContext)
            {
                var sut = new CabRepository(dbContext, _domainModelMapper, _persistenceModelMapper);
                await sut.Get(cabId);

                // Assert
                _persistenceModelMapper
                    .Received(1)
                    .Map(Arg.Is<PersistenceModel.Cab>(x => x.Id == cab.Id));
            }
        }


        [TestMethod]
        public async Task WhenItemDoesNotExists_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var cabId = _fixture.Create<Id<Cab>>();
            var dbContext = new DispatchingDbContextBuilder()
                .Build();

            // Act
            using (dbContext)
            {
                var sut = new CabRepository(dbContext, _domainModelMapper, _persistenceModelMapper);
                Func<Task> act = async () => await sut.Get(cabId);

                // Assert
                act.Should().Throw<KeyNotFoundException>();
            }
        }
    }
}
