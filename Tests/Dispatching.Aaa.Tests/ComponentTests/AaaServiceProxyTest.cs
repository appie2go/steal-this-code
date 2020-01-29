using AutoFixture;
using Dispatching.Aaa.Configuration;
using Dispatching.Aaa.DataContract;
using Dispatching.Configuration;
using Dispatching.Rides;
using Dispatching.Rides.Processes.SecondaryPorts;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dispatching.Aaa.Tests.ComponentTests
{
    [TestClass]
    public class AaaServiceProxyTest
    {
        private readonly Fixture _fixture = new Fixture();

        private IHttpClient _httpClient;

        private IProvideTrafficInformation _sut;

        [TestInitialize]
        public void Initialize()
        {
            _httpClient = Substitute.For<IHttpClient>();

            // Bootstrap
            var serviceProvider = new ServiceCollection()
                .UseDispatching()
                .UseAaaTrafficInformation()
                .AddTransient((s) => _httpClient)
                .BuildServiceProvider();

            _sut = serviceProvider.GetService<IProvideTrafficInformation>();
        }

        [TestMethod]
        public async Task WhenTrafficInformation_ShouldGetDistanceBetweenLocations()
        {
            // Arrange
            var response = _fixture.Create<GetDistanceResponse>();

            _httpClient
                .PostAsync(Arg.Any<string>(), Arg.Any<HttpContent>())
                .Returns(response.ToHttpResponseMessage());

            var from = _fixture.Create<Location>();
            var to = _fixture.Create<Location>();

            // Act
            var actual = await _sut.GetDistanceBetweenLocations(from, to);

            // Assert
            actual.Should().NotBeNull();
        }


        [TestMethod]
        public async Task WhenTrafficInformation_ShouldGetEstimatedTimeOfArrival()
        {
            // Arrange
            var response = _fixture.Create<EstimatedTimeOfArrivalResponse>();

            _httpClient
                .PostAsync(Arg.Any<string>(), Arg.Any<HttpContent>())
                .Returns(response.ToHttpResponseMessage());

            var departureTime = _fixture.Create<DateTime>();
            var distanceToCover = _fixture.Create<Kilometer>();

            // Act
            var actual = await _sut.GetTimeOfArival(departureTime, distanceToCover);

            // Assert
            actual.Should().NotBe(DateTime.MinValue);
        }
    }
}
