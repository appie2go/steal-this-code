using AutoFixture;
using Dispatching.Aaa.Mappers;
using Dispatching.Rides;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dispatching.Aaa.Tests.UnitTests.AaaServiceProxyTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class GetDistanceBetweenLocationsTest
    {
        private readonly Fixture _fixture = new Fixture();

        private IHttpClient _httpClient;
        private IEstimatedTimeOfArrivalRequestMapper _estimatedTimeOfArrivalRequestMapper;
        private IEstimatedTimeOfArrivalResponseMapper _estimatedTimeOfArrivalResponseMapper;
        private IGetDistanceRequestMapper _getDistanceRequestMapper;
        private IGetDistanceResponseMapper _getDistanceResponseMapper;

        private Location _from;
        private Location _to;

        private AaaServiceProxy _sut;

        [TestInitialize]
        public void Initialize()
        {
            _httpClient = Substitute.For<IHttpClient>();
            _estimatedTimeOfArrivalRequestMapper = Substitute.For<IEstimatedTimeOfArrivalRequestMapper>();
            _estimatedTimeOfArrivalResponseMapper = Substitute.For<IEstimatedTimeOfArrivalResponseMapper>();
            _getDistanceRequestMapper = Substitute.For<IGetDistanceRequestMapper>();
            _getDistanceResponseMapper = Substitute.For<IGetDistanceResponseMapper>();

            _from = _fixture.Create<Location>();
            _to = _fixture.Create<Location>();

            _sut = new AaaServiceProxy(_httpClient, _estimatedTimeOfArrivalRequestMapper, _estimatedTimeOfArrivalResponseMapper, _getDistanceRequestMapper, _getDistanceResponseMapper);
        }

        [TestMethod]
        public async Task WhenLocations_ShouldMapIntoRequest()
        {
            // Act
            await _sut.GetDistanceBetweenLocations(_from, _to);

            // Assert
            _getDistanceRequestMapper
                .Received(1)
                .Map(Arg.Is(_from), Arg.Is(_to));
        }

        [TestMethod]
        public async Task WhenLocations_ShouldInvokeAaaEndpoint()
        {
            // Arrange
            var expectedEndpoint = "http://api.aaa.com/api/distance";
            var expectedRequest = new StringContent(_fixture.Create<string>());
            
            _getDistanceRequestMapper
                .Map(Arg.Any<Location>(), Arg.Any<Location>())
                .Returns(expectedRequest);

            // Act
            await _sut.GetDistanceBetweenLocations(_from, _to);

            // Assert
            await _httpClient
                .Received(1)
                .PostAsync(Arg.Is(expectedEndpoint), Arg.Is(expectedRequest));
        }

        [TestMethod]
        public async Task WhenResponse_ShouldMapIntoKilometers()
        {
            // Arrange
            var expected = new HttpResponseMessage();
            _httpClient
                .PostAsync(Arg.Any<string>(), Arg.Any<HttpContent>())
                .Returns(expected);

            // Act
            await _sut.GetDistanceBetweenLocations(_from, _to);

            // Assert
            _getDistanceResponseMapper
                .Received(1)
                .Map(Arg.Is(expected));
        }


        [TestMethod]
        public async Task WhenResponse_ShouldReturnWhateverResponseMapperReturns()
        {
            // Arrange
            var expected = _fixture.Create<Kilometer>();
            _getDistanceResponseMapper
                .Map(Arg.Any<HttpResponseMessage>())
                .Returns(expected);

            // Act
            var actual = await _sut.GetDistanceBetweenLocations(_from, _to);

            // Assert
            actual.Should().Be(expected);
        }
    }
}
