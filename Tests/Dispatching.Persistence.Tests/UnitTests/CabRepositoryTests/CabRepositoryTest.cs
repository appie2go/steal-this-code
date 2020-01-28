using AutoFixture;
using Dispatching.Cabs;
using Dispatching.Persistence.Mappers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;

namespace Dispatching.Persistence.Tests.UnitTests.CabRepositoryTests
{
    [TestClass]
    public class CabRepositoryTest : IDisposable
    {
        private readonly Fixture _fixture = new Fixture();

        private DispatchingDbContext _context;
        private IMapToPersistenceModel<Cab, PersistenceModel.Cab> _domainModelMapper;
        private IMapToDomainModel<PersistenceModel.Cab, Cab> _persistenceModelMapper;

        [TestInitialize]
        public void Initialize()
        {
            _context = new InMemoryDispatchingDbContext(_fixture.Create<string>());
            _domainModelMapper = Substitute.For<IMapToPersistenceModel<Cab, PersistenceModel.Cab>>();
            _persistenceModelMapper = Substitute.For<IMapToDomainModel<PersistenceModel.Cab, Cab>>();
        }

        [TestMethod]
        public void WhenDbContextNull_ShouldThrowArgumentNullException()
        {
            // Act
            Action act = () => new CabRepository(null, _domainModelMapper, _persistenceModelMapper);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void WhenDomainModelMapperNull_ShouldThrowArgumentNullException()
        {
            // Act
            Action act = () => new CabRepository(_context, null, _persistenceModelMapper);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void WhenPersistenceModelMapperNull_ShouldThrowArgumentNullException()
        {
            // Act
            Action act = () => new CabRepository(_context, _domainModelMapper, null);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
