using AutoFixture;
using Dispatching.Framework;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


namespace Dispatching.Tests.UnitTests.Framework.Id
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class InequalityTest
    {
        private readonly Fixture _fixture = new Fixture();

        private Guid _guid;

        [TestInitialize]
        public void Initialize()
        {
            _guid = _fixture.Create<Guid>();
        }

        [TestMethod]
        public void WhenSameGuid_ComparisonShouldBeFalse()
        {
            // Arrange
            var instance1 = new Id<TestEntity>(_guid);
            var instance2 = new Id<TestEntity>(_guid);

            // Act
            var actual = instance1 != instance2;

            // Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void WhenDifferentGuid_ComparisonShouldBeTrue()
        {
            // Arrange
            var instance1 = new Id<TestEntity>(_guid);
            var instance2 = new Id<TestEntity>(_fixture.Create<Guid>());

            // Act
            var actual = instance1 != instance2;

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenSameInstance_ComparisonShouldBeFalse()
        {
            // Arrange
            var instance = new Id<TestEntity>(_guid);

            // Act
#pragma warning disable CS1718 // Comparison made to same variable
            var actual = instance != instance;
#pragma warning disable CS1718 // Comparison made to same variable

            // Assert
            actual.Should().BeFalse();
        }

        private class TestEntity : Entity<TestEntity>
        {
            public TestEntity(Id<TestEntity> id) : base(id)
            {
            }
        }
    }
}
