using AutoFixture;
using DomainDrivenDesign.DomainObjects;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Dispatching.Tests.UnitTests.Framework.Id
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class ToGuidTest
    {
        private readonly Fixture _fixture = new Fixture();

        [TestMethod]
        public void WhenIdProvided_ShouldSetId()
        {
            // Arrange
            var guid = _fixture.Create<Guid>();

            // Act
            var actual = new Id<TestEntity>(guid);

            // Assert
            actual.ToGuid().Should().Be(guid);
        }

        private class TestEntity : Entity<TestEntity>
        {
            public TestEntity(Id<TestEntity> id) : base(id)
            {

            }
        }
    }
}
