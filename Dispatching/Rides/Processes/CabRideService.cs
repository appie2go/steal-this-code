using Dispatching.Customers;
using Dispatching.Framework;
using Dispatching.Rides.Processes.PrimaryPorts;
using Dispatching.Rides.Processes.SecondaryPorts;
using System.Threading.Tasks;

namespace Dispatching.Rides.Processes
{ 
    internal class CabRideService : ICabRideService
    {
        private readonly IProvideLocation _locationProvider;
        private readonly IProvideCab _cabProvider;
        private readonly IProvideTime _timeProvider;
        private readonly IProvideTrafficInformation _trafficInformationProvider;

        internal CabRideService(IProvideLocation locationProvider,
                                IProvideCab cabProvider,
                                IProvideTime timeProvider,
                                IProvideTrafficInformation trafficInformationProvider)
        {
            _locationProvider = locationProvider;
            _cabProvider = cabProvider;
            _timeProvider = timeProvider;
            _trafficInformationProvider = trafficInformationProvider;
        }

        public async Task<Ride> BringCustomerToTheTrainStation(Id<Customer> customerId, Location customerLocation)
        {
            var currentTime = await _timeProvider.GetCurrentTime();

            // Find a cab
            var cab = await _cabProvider.GetNearestAvailableCab(customerLocation);
            cab.GoTo(customerLocation);
            cab.Embarc(customerId);

            // Bring the customer to the trainstation
            var rideId = Id<Ride>.CreateNew();
            var destination = await _locationProvider.GetTrainStationLocation();
            var ride = new Ride(rideId, customerId, cab.Id);
            ride.SetDestination(destination);
            ride.Start(currentTime);

            var distance = await _trafficInformationProvider.GetDistanceBetweenLocations(destination, customerLocation);
            var eta = await  _trafficInformationProvider.GetTimeOfArival(currentTime, distance);
            ride.Stop(eta);

            // Update the cab administration
            await _cabProvider.Update(cab);

            return ride;
        }
    }
}
