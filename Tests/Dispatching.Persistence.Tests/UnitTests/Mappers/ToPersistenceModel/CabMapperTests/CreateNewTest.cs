using AutoFixture;
using Dispatching.Persistence.Mappers.ToPersistanceModel;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dispatching.Persistence.Tests.UnitTests.Mappers.ToPersistenceModel.CabMapperTests
{
    [TestClass]
    public class CreateNewTest
    {
        private readonly Fixture _fixture = new Fixture();

        private CabMapper _sut;
        
        [TestInitialize]
        public void Initialize()
        {
            _sut = new CabMapper();
        }

        [TestMethod]
        public void ShouldNotReturnNull()
        {
            // Act
            var actual = _sut.CreateNew();

            // Assert
            actual.Should().NotBeNull();
        }


        [TestMethod]
        public void ShouldNotReturnItemWithId()
        {
            // Act
            var actual = _sut.CreateNew();

            // Assert
            actual.Id.Should().BeEmpty();
        }
    }
}
