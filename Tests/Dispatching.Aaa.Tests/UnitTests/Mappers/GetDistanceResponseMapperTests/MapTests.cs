using AutoFixture;
using Dispatching.Aaa.Mappers;
using Dispatching.Rides;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Dispatching.Aaa.Tests.UnitTests.Mappers.GetDistanceResponseMapperTests
{
    [TestClass]
    [TestCategory("UnitTests")]
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


        [TestMethod]
        public void WhenAaaDoesNotReturn200ok_ShouldThrowException()
        {
            // Arrange
            var httpResponseMessage = _theirResponse.ToHttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);

            // Act
            Action act = () => _mapper.Map(httpResponseMessage);

            // Assert
            act.Should().Throw<ApplicationException>();
        }


        public class TheirSchema
        {
            public int Kilometers { get; set; }
        }
    }
}
