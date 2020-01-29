using AutoFixture;
using Dispatching.Rides;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dispatching.Tests.UnitTests.Rides.KilometerTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class OperatorTests
    {
        private readonly Fixture _fixture = new Fixture();

        private Kilometer _distance1;
        private decimal _distance1InDecimals;

        private Kilometer _distance2;
        private decimal _distance2InDecimals;

        [TestInitialize]
        public void Initialize()
        {
            _distance1InDecimals = _fixture.Create<decimal>() * _fixture.Create<decimal>();
            _distance1 = Kilometer.FromDecimal(_distance1InDecimals);

            _distance2InDecimals = _fixture.Create<decimal>();
            _distance2 = Kilometer.FromDecimal(_distance2InDecimals);
        }

        [TestMethod]
        public void WhenAdding_ResultShouldBeSumOfDistances()
        {
            // Act
            var actual = _distance1 + _distance2;

            // Assert
            var expected = Kilometer.FromDecimal(_distance1InDecimals + _distance2InDecimals);
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void WhenSubtracting_ResultShouldBeDistanceMinusOtherDistance()
        {
            // Act
            var actual = _distance1 - _distance2;

            // Assert
            var expected = Kilometer.FromDecimal(_distance1InDecimals - _distance2InDecimals);
            actual.Should().Be(expected);
        }

    }
}
