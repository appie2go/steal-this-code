using AutoFixture;
using Dispatching.Framework;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Dispatching.Tests.UnitTests.Framework.Id
{
    [TestClass]
    public class ToStringTest
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
            actual.ToString().Should().Be(guid.ToString());
        }

        private class TestEntity : Entity<TestEntity>
        {
            public TestEntity(Id<TestEntity> id) : base(id)
            {

            }
        }
    }
}
