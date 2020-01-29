using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Dispatching.Rides.Processes;
using Dispatching.Rides.Processes.SecondaryPorts;
using NSubstitute;
using FluentAssertions;

namespace Dispatching.Tests.UnitTests.Rides.Processes.CabRideServiceTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class CabRideServiceTest
    {
        private IProvideLocation _locationProvider;
        private IProvideCab _cabProvider;
        private IProvideTime _timeProvider;
        private IProvideTrafficInformation _trafficInformationProvider;

        [TestInitialize]
        public void Initialize()
        {
            _locationProvider = Substitute.For<IProvideLocation>();
            _cabProvider = Substitute.For<IProvideCab>();
            _timeProvider = Substitute.For<IProvideTime>();
            _trafficInformationProvider = Substitute.For<IProvideTrafficInformation>();
        }

        [TestMethod]
        public void WhenLocationProviderNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            IProvideLocation locationProvider = null;

            // Act
            Action act = () => new CabRideService(locationProvider, _cabProvider, _timeProvider, _trafficInformationProvider);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void WhenCabProviderNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            IProvideCab cabProvider = null;

            // Act
            Action act = () => new CabRideService(_locationProvider, cabProvider, _timeProvider, _trafficInformationProvider);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void WhenTimeProviderNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            IProvideTime timeProvider = null;

            // Act
            Action act = () => new CabRideService(_locationProvider, _cabProvider, timeProvider, _trafficInformationProvider);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void WhenTrafficInformationProviderNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            IProvideTrafficInformation trafficInformationProvider = null;

            // Act
            Action act = () => new CabRideService(_locationProvider, _cabProvider, _timeProvider, trafficInformationProvider);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }
    }
}
