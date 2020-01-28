using AutoFixture;
using Dispatching.Framework;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Dispatching.Tests.UnitTests.Framework.Id
{
    [TestClass]
    public class EqualityTest
    {
        private readonly Fixture _fixture = new Fixture();

        private Guid _guid;

        [TestInitialize]
        public void Initialize()
        {
            _guid = _fixture.Create<Guid>();
        }

        [TestMethod]
        public void WhenSameGuid_ComparisonShouldBeTrue()
        {
            // Arrange
            var instance1 = new Id<TestEntity>(_guid);
            var instance2 = new Id<TestEntity>(_guid);

            // Act
            var actual = instance1 == instance2;

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenDifferentGuid_ComparisonShouldBeFalse()
        {
            // Arrange
            var instance1 = new Id<TestEntity>(_guid);
            var instance2 = new Id<TestEntity>(_fixture.Create<Guid>());

            // Act
            var actual = instance1 == instance2;

            // Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void WhenSameInstance_ComparisonShouldBeTrue()
        {
            // Arrange
            var instance = new Id<TestEntity>(_guid);

            // Act
            var actual = instance == instance;

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenSameGuid_ShouldBeEqual()
        {
            // Arrange
            var instance1 = new Id<TestEntity>(_guid);
            var instance2 = new Id<TestEntity>(_guid);

            // Act
            var actual = instance1.Equals(instance2);

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenDifferentGuid_ShouldNotBeEqual()
        {
            // Arrange
            var instance1 = new Id<TestEntity>(_guid);
            var instance2 = new Id<TestEntity>(_fixture.Create<Guid>());

            // Act
            var actual = instance1.Equals(instance2);

            // Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void WhenSameInstance_ShouldBeEqual()
        {
            // Arrange
            var instance = new Id<TestEntity>(_guid);

            // Act
            var actual = instance.Equals(instance);

            // Assert
            actual.Should().BeTrue();
        }


        [TestMethod]
        public void WhenCastedToObjectAndSameGuid_ShouldBeEqual()
        {
            // Arrange
            var instance1 = new Id<TestEntity>(_guid);
            var instance2 = new Id<TestEntity>(_guid);

            // Act
            var actual = instance1.Equals((object)instance2);

            // Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void WhenCastedToObjectAndDifferentGuid_ShouldNotBeEqual()
        {
            // Arrange
            var instance1 = new Id<TestEntity>(_guid);
            var instance2 = new Id<TestEntity>(_fixture.Create<Guid>());

            // Act
            var actual = instance1.Equals((object)instance2);

            // Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void WhenCastedToObjectAndSameInstance_ShouldBeEqual()
        {
            // Arrange
            var instance = new Id<TestEntity>(_guid);

            // Act
            var actual = instance.Equals((object)instance);

            // Assert
            actual.Should().BeTrue();
        }


        [TestMethod]
        public void WhenDifferentObjects_ShouldHaveDifferentHashcodes()
        {
            // Arrange
            var id = _fixture.Create<Id<TestEntity>>();
            var otherId = _fixture.Create<Id<TestEntity>>();

            // Act
            var actual1 = id.GetHashCode();
            var actual2 = otherId.GetHashCode();

            // Asset
            actual1.Should().NotBe(actual2);
        }

        private class TestEntity : Entity<TestEntity>
        {
            public TestEntity(Id<TestEntity> id) : base(id)
            {
            }
        }
    }
}
