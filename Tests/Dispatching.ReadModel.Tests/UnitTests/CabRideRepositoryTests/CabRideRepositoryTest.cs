using AutoFixture;
using Dispatching.Persistence.Tests;
using Dispatching.ReadModel.Mappers;
using Dispatching.ReadModel.PersistenceModel;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;

namespace Dispatching.ReadModel.Tests.UnitTests.CabRideRepositoryTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class CabRideRepositoryTest
    {
        private Fixture _fixture = new Fixture();

        private IApply<CabRide> _mapper;
        private DispatchingReadDbContext _dbContext;

        [TestInitialize]
        public void Initialize()
        {
            _mapper = Substitute.For<IApply<CabRide>>();
            _dbContext = new InMemoryDispatchingDbContext(_fixture.Create<string>());
        }

        [TestMethod]
        public void WhenNoDbContext_ShouldThrowArgumentNullException()
        {
            // Arrange
            DispatchingReadDbContext dbContext = null;

            // Act
            Action act = () => new CabRideRepository(dbContext, _mapper);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void WhenNoMapper_ShouldThrowArgumentNullException()
        {
            // Arrange
            IApply<CabRide> mapper = null;

            // Act
            Action act = () => new CabRideRepository(_dbContext, mapper);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }
    }
}
