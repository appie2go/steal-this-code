using AutoFixture;
using Dispatching.Aaa.Mappers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;

namespace Dispatching.Aaa.Tests.UnitTests.Mappers.EstimatedTimeOfArrivalResponseMapperTests
{
    [TestClass]
    public class MapTest
    {
        private readonly Fixture _fixture = new Fixture();

        private TheirSchema _theirResponse;

        private EstimatedTimeOfArrivalResponseMapper _mapper;

        [TestInitialize]
        public void Initialize()
        {
            _theirResponse = _fixture.Create<TheirSchema>();

            _mapper = new EstimatedTimeOfArrivalResponseMapper();
        }

        [TestMethod]
        public void WhenAaaPopulatesEtaField_ShouldDeserializeTheirJsonToDateTimeObject()
        {
            // Arrange
            var httpResponseMessage = _theirResponse.ToHttpResponseMessage();

            // Act
            var actual = _mapper.Map(httpResponseMessage);

            // Assert
            actual.Should().Be(_theirResponse.Eta);
        }

        [TestMethod]
        public void WhenAaaDoesNotReturnStatusCode200Ok_ShouldThrowApplicationException()
        {
            // Arrange
            var httpResponseMessage = _theirResponse.ToHttpResponseMessage(HttpStatusCode.InternalServerError);

            // Act
            Action act = () => _mapper.Map(httpResponseMessage);

            // Assert
            act.Should().Throw<ApplicationException>();
        }

        public class TheirSchema
        {
            public DateTime Eta { get; set; }

            public int TrafficIntensity { get; set; }
        }
    }
}
