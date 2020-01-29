using AutoFixture;
using Dispatching.Aaa.Mappers;
using Dispatching.Rides;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dispatching.Aaa.Tests.UnitTests.Mappers.GetDistanceResponseMapperTests
{
    [TestClass]
    public class MapTests
    {
        private readonly Fixture _fixture = new Fixture();

        private TheirSchema _theirResponse;

        private GetDistanceResponseMapper _mapper;

        [TestInitialize]
        public void Initialize()
        {
            _theirResponse = _fixture.Create<TheirSchema>();

            _mapper = new GetDistanceResponseMapper();
        }

        [TestMethod]
        public void WhenAaaPopulatesEtaField_ShouldDeserializeTheirJsonToKilometers()
        {
            // Arrange
            var httpResponseMessage = _theirResponse.ToHttpResponseMessage();

            // Act
            var actual = _mapper.Map(httpResponseMessage);

            // Assert
            var expected = Kilometer.FromDecimal(_theirResponse.Kilometers);
            actual.Should().Be(expected);
        }

        public class TheirSchema
        {
            public int Kilometers { get; set; }
        }
    }
}
