using AutoFixture;
using Dispatching.Rides;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Dispatching.Tests.UnitTests.Rides.EuroTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class EuroTests
    {
        private readonly Fixture _fixture = new Fixture();

        [TestMethod]
        public void WhenNegativeAmount_ShouldThrowArgumentException()
        {
            // Arrange
            var negativeAmount = -1 * _fixture.Create<decimal>();

            // Act
            Action act = () => Euro.FromDecimal(negativeAmount);

            // Asset
            act.Should().Throw<ArgumentException>("Amount must be bigger than 0.");
        }

        [TestMethod]
        public void WhenAmountProvided_ShouldSetAmount()
        {
            // Arrange
            var amount = _fixture.Create<decimal>();

            // Act
            var actual = Euro.FromDecimal(amount);

            // Assert
            actual.ToDecimal().Should().Be(amount);
        }

        [TestMethod]
        public void WhenAmountProvided_ShouldDisplayAmmountAsCurrency()
        {
            // Arrange
            var amount = _fixture.Create<decimal>();

            // Act
            var actual = Euro.FromDecimal(amount);

            // Assert
            actual.ToString().Should().Be(amount.ToString("c"));
        }

        [TestMethod]
        public void WhenEuroNone_ShouldBe0Euro()
        {
            // Act
            var actual = Euro.None;

            // Assert
            actual.Should().Be(Euro.FromDecimal(0));
        }
    }
}
