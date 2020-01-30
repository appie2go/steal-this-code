using AutoFixture;
using Dispatching.Cabs;
using Dispatching.Customers;
using Dispatching.Framework;
using Dispatching.Rides;
using Dispatching.Rides.Processes;
using Dispatching.Rides.Processes.SecondaryPorts;
using Dispatching.TestFixtures.DomainObjects;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Threading.Tasks;

namespace Dispatching.Tests.UnitTests.Rides.Processes.CabRideServiceTests
{
    [TestClass]
    [TestCategory("UnitTests")]
    public class BringCustomerToTheTrainStationTest
    {
        private readonly Fixture _fixture = new Fixture();

        private Id<Customer> _customerId;
        private Location _customerLocation;

        private Cab _cab;
        private DateTime _currentTime;
        private Location _trainStationLocation;
        private DateTime _estimatedTimeOfArrival;

        private IProvideLocation _locationProvider;
        private IProvideCab _cabProvider;
        private IProvideTime _timeProvider;
        private IProvideTrafficInformation _trafficInformationProvider;

        private CabRideService _sut;

        [TestInitialize]
        public void Initialize()
        {
            _fixture.Customize(new LocationCustomization());

            // Create test data
            _customerId = _fixture.Create<Id<Customer>>();
            _customerLocation = _fixture.Create<Location>();

            _cab = _fixture.Create<Cab>();
            _trainStationLocation = _fixture.Create<Location>();
            _currentTime = _fixture.Create<DateTime>();
            _estimatedTimeOfArrival = _currentTime.AddMinutes(_fixture.Create<int>());

            // Setup stubs
            _locationProvider = Substitute.For<IProvideLocation>();
            _locationProvider.GetTrainStationLocation().Returns(_trainStationLocation);

            _cabProvider = Substitute.For<IProvideCab>();
            _cabProvider.GetNearestAvailableCab(Arg.Any<Location>()).Returns(_cab);

            _timeProvider = Substitute.For<IProvideTime>();
            _timeProvider.GetCurrentTime().Returns(_currentTime);

            _trafficInformationProvider = Substitute.For<IProvideTrafficInformation>();
            _trafficInformationProvider.GetTimeOfArival(Arg.Any<DateTime>(), Arg.Any<Kilometer>()).Returns(_estimatedTimeOfArrival);
            _trafficInformationProvider.GetDistanceBetweenLocations(Arg.Any<Location>(), Arg.Any<Location>()).Returns(_fixture.Create<Kilometer>());

            // Create subject under test
            _sut = new CabRideService(_locationProvider, _cabProvider, _timeProvider, _trafficInformationProvider);
        }

        [TestMethod]
        public async Task CabShouldPickupCustomer()
        {
            // Act
            await _sut.BringCustomerToTheTrainStation(_customerId, _customerLocation);

            // Assert
            _cab.CurrentLocation.Should().Be(_trainStationLocation);
        }

        [TestMethod]
        public async Task CabShouldDriveToTrainstation()
        {
            // Act
            await _sut.BringCustomerToTheTrainStation(_customerId, _customerLocation);

            // Assert
            _cab.Passengers.Should().Contain(_customerId);
        }

        [TestMethod]
        public async Task WhenCustomerId_RideShouldContainCustomerId()
        {
            // Act
            var actual = await _sut.BringCustomerToTheTrainStation(_customerId, _customerLocation);

            // Assert
            actual.CustomerId.Should().Be(_customerId);
        }

        [TestMethod]
        public async Task RideDestinationShouldBeSetToTrainstation()
        {
            // Act
            var actual = await _sut.BringCustomerToTheTrainStation(_customerId, _customerLocation);

            // Assert
            actual.Destination.Should().Be(_trainStationLocation);
        }

        [TestMethod]
        public async Task RideShouldHaveStartedImmidiately()
        {
            // Act
            var actual = await _sut.BringCustomerToTheTrainStation(_customerId, _customerLocation);

            // Assert
            actual.Started.Should().Be(_currentTime);
        }

        [TestMethod]
        public async Task RideShouldHaveStoppedAtTheEstimatedTimeOfArrival()
        {
            // Act
            var actual = await _sut.BringCustomerToTheTrainStation(_customerId, _customerLocation);

            // Assert
            actual.Stopped.Should().Be(_estimatedTimeOfArrival);
        }

        [TestMethod]
        public async Task CabInfromationShouldBeUpdated()
        {
            // Act
            var actual = await _sut.BringCustomerToTheTrainStation(_customerId, _customerLocation);

            // Assert
            await _cabProvider
                .Received(1)
                .Update(Arg.Is(_cab));
        }
    }
}
