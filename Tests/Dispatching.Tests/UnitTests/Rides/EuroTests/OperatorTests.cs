using AutoFixture;
using Dispatching.Rides;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Dispatching.Tests.UnitTests.Rides.EuroTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class OperatorTests
    {
        private readonly Fixture _fixture = new Fixture();

        private Euro _amount;
        private decimal _amountInDecimals;

        [TestInitialize]
        public void Initialize()
        {
            _amountInDecimals = _fixture.Create<decimal>();
            _amount = Euro.FromDecimal(_amountInDecimals);
        }

        [TestMethod]
        public void WhenDevidingByDecimal_ResultShouldEqualAmountDevidedByFactor()
        {
            // Arrange
            var factor = _fixture.Create<decimal>();

            // Act
            var actual = _amount / factor;

            // Assert
            var expected = Euro.FromDecimal(_amountInDecimals / factor);
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void WhenDevidingByDecimalByZero_ShouldThrowDevideByZeroException()
        {
            // Arrange
            var factor = 0m;

            // Act
            Action act = () => { var x = _amount / factor; };

            // Assert
            act.Should().Throw<DivideByZeroException>();
        }

        [TestMethod]
        public void WhenMultiplyingByDecimal_ResultShouldEqualAmountMultipliedByFactor()
        {
            // Arrange
            var factor = _fixture.Create<decimal>();

            // Act
            var actual = _amount * factor;

            // Assert
            var expected = Euro.FromDecimal(_amountInDecimals * factor);
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void WhenDevidingByDouble_ResultShouldEqualAmountDevidedByFactor()
        {
            // Arrange
            var factor = _fixture.Create<double>();

            // Act
            var actual = _amount / factor;

            // Assert
            var expected = Euro.FromDecimal(_amountInDecimals / (decimal)factor);
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void WhenDevidingByDoubleByZero_ShouldThrowDevideByZeroException()
        {
            // Arrange
            var factor = 0d;

            // Act
            Action act = () => { var x = _amount / factor; };

            // Assert
            act.Should().Throw<DivideByZeroException>();
        }

        [TestMethod]
        public void WhenMultiplyingByDouble_ResultShouldEqualAmountMultipliedByFactor()
        {
            // Arrange
            var factor = _fixture.Create<double>();

            // Act
            var actual = _amount * factor;

            // Assert
            var expected = Euro.FromDecimal(_amountInDecimals * (decimal)factor);
            actual.Should().Be(expected);
        }
    }
}
