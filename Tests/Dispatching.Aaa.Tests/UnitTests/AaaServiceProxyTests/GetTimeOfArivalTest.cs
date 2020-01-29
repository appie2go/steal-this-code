using AutoFixture;
using Dispatching.Aaa.Mappers;
using Dispatching.Rides;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dispatching.Aaa.Tests.UnitTests.AaaServiceProxyTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class GetTimeOfArivalTest
    {
        private readonly Fixture _fixture = new Fixture();

        private IHttpClient _httpClient;
        private IEstimatedTimeOfArrivalRequestMapper _estimatedTimeOfArrivalRequestMapper;
        private IEstimatedTimeOfArrivalResponseMapper _estimatedTimeOfArrivalResponseMapper;
        private IGetDistanceRequestMapper _getDistanceRequestMapper;
        private IGetDistanceResponseMapper _getDistanceResponseMapper;

        private DateTime _departureTime;
        private Kilometer _distanceToCover;

        private AaaServiceProxy _sut;

        [TestInitialize]
        public void Initialize()
        {
            _httpClient = Substitute.For<IHttpClient>();
            _estimatedTimeOfArrivalRequestMapper = Substitute.For<IEstimatedTimeOfArrivalRequestMapper>();
            _estimatedTimeOfArrivalResponseMapper = Substitute.For<IEstimatedTimeOfArrivalResponseMapper>();
            _getDistanceRequestMapper = Substitute.For<IGetDistanceRequestMapper>();
            _getDistanceResponseMapper = Substitute.For<IGetDistanceResponseMapper>();

            _departureTime = _fixture.Create<DateTime>();
            _distanceToCover = _fixture.Create<Kilometer>();

            _sut = new AaaServiceProxy(_httpClient, _estimatedTimeOfArrivalRequestMapper, _estimatedTimeOfArrivalResponseMapper, _getDistanceRequestMapper, _getDistanceResponseMapper);
        }

        [TestMethod]
        public async Task WhenDepartureTimeAndDistanceToCover_ShouldMapIntoRequest()
        {
            // Act
            await _sut.GetTimeOfArival(_departureTime, _distanceToCover);

            // Assert
            _estimatedTimeOfArrivalRequestMapper
                .Received(1)
                .Map(Arg.Is(_departureTime), Arg.Is(_distanceToCover));
        }

        [TestMethod]
        public async Task WhenDepartureTimeAndDistanceToCover_ShouldInvokeAaaEndpoint()
        {
            // Arrange
            var expectedEndpoint = "http://api.aaa.com/api/eta";
            var expectedRequest = new StringContent(_fixture.Create<string>());

            _estimatedTimeOfArrivalRequestMapper
                .Map(Arg.Any<DateTime>(), Arg.Any<Kilometer>())
                .Returns(expectedRequest);

            // Act
            await _sut.GetTimeOfArival(_departureTime, _distanceToCover);

            // Assert
            await _httpClient
                .Received(1)
                .PostAsync(Arg.Is(expectedEndpoint), Arg.Is(expectedRequest));
        }

        [TestMethod]
        public async Task WhenResponse_ShouldMapIntoDateTime()
        {
            // Arrange
            var expected = new HttpResponseMessage();
            _httpClient
                .PostAsync(Arg.Any<string>(), Arg.Any<HttpContent>())
                .Returns(expected);

            // Act
            await _sut.GetTimeOfArival(_departureTime, _distanceToCover);

            // Assert
            _estimatedTimeOfArrivalResponseMapper
                .Received(1)
                .Map(Arg.Is(expected));
        }

        [TestMethod]
        public async Task WhenResponse_ShouldReturnWhateverResponseMapperReturns()
        {
            // Arrange
            var expected = _fixture.Create<DateTime>();
            _estimatedTimeOfArrivalResponseMapper
                .Map(Arg.Any<HttpResponseMessage>())
                .Returns(expected);

            // Act
            var actual = await _sut.GetTimeOfArival(_departureTime, _distanceToCover);

            // Assert
            actual.Should().Be(expected);
        }
    }
}
