using Dispatching.Aaa.Mappers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;

namespace Dispatching.Aaa.Tests.UnitTests.AaaServiceProxyTests
{
    [TestClass]
    public class AaaServiceProxyTest
    {
        private IHttpClient _httpClient;
        private IEstimatedTimeOfArrivalRequestMapper _estimatedTimeOfArrivalRequestMapper;
        private IEstimatedTimeOfArrivalResponseMapper _estimatedTimeOfArrivalResponseMapper;
        private IGetDistanceRequestMapper _getDistanceRequestMapper;
        private IGetDistanceResponseMapper _getDistanceResponseMapper;

        [TestInitialize]
        public void Initialize()
        {
            _httpClient = Substitute.For<IHttpClient>();
            _estimatedTimeOfArrivalRequestMapper = Substitute.For<IEstimatedTimeOfArrivalRequestMapper>();
            _estimatedTimeOfArrivalResponseMapper = Substitute.For<IEstimatedTimeOfArrivalResponseMapper>();
            _getDistanceRequestMapper = Substitute.For<IGetDistanceRequestMapper>();
            _getDistanceResponseMapper = Substitute.For<IGetDistanceResponseMapper>();
        }

        [TestMethod]
        public void WhenNoHttpClient_ShouldThrowArgumentNullException()
        {
            // Arrange
            IHttpClient httpClient = null;

            // Act
            Action act = () => new AaaServiceProxy(httpClient, _estimatedTimeOfArrivalRequestMapper, _estimatedTimeOfArrivalResponseMapper, _getDistanceRequestMapper, _getDistanceResponseMapper);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void WhenNoEstimatedTimeOfArrivalRequestMapper_ShouldThrowArgumentNullException()
        {
            // Arrange
            IEstimatedTimeOfArrivalRequestMapper estimatedTimeOfArrivalRequestMapper = null;

            // Act
            Action act = () => new AaaServiceProxy(_httpClient, estimatedTimeOfArrivalRequestMapper, _estimatedTimeOfArrivalResponseMapper, _getDistanceRequestMapper, _getDistanceResponseMapper);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void WhenNoEstimatedTimeOfArrivalResponseMapper_ShouldThrowArgumentNullException()
        {
            // Arrange
            IEstimatedTimeOfArrivalResponseMapper estimatedTimeOfArrivalResponseMapper = null;

            // Act
            Action act = () => new AaaServiceProxy(_httpClient, _estimatedTimeOfArrivalRequestMapper, estimatedTimeOfArrivalResponseMapper, _getDistanceRequestMapper, _getDistanceResponseMapper);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void WhenNoGetDistanceRequestMapper_ShouldThrowArgumentNullException()
        {
            // Arrange
            IGetDistanceRequestMapper getDistanceRequestMapper = null;

            // Act
            Action act = () => new AaaServiceProxy(_httpClient, _estimatedTimeOfArrivalRequestMapper, _estimatedTimeOfArrivalResponseMapper, getDistanceRequestMapper, _getDistanceResponseMapper);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }


        [TestMethod]
        public void WhenNoGetDistanceResponseMapper_ShouldThrowArgumentNullException()
        {
            // Arrange
            IGetDistanceResponseMapper getDistanceResponseMapper = null;

            // Act
            Action act = () => new AaaServiceProxy(_httpClient, _estimatedTimeOfArrivalRequestMapper, _estimatedTimeOfArrivalResponseMapper, _getDistanceRequestMapper, getDistanceResponseMapper);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }
    }
}
