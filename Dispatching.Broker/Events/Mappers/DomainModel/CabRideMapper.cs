using Dispatching.Broker.Commands;
using Dispatching.Rides;
using System;

namespace Dispatching.Broker.Events.Mappers.DomainModel
{
    internal class CabRideMapper : ICabRideMapper
    {
        public DriveCustomerToTrainStationFailed MapFailedEvent(DriveCustomerToTrainStation command, Exception error)
        {
            return new DriveCustomerToTrainStationFailed
            {
                Reason = error.ToString()
            };
        }

        public DroveCustomerToTrainStation MapSuccessEvent(Ride ride)
        {
            return new DroveCustomerToTrainStation
            {
                CabRideId = ride.Id.ToGuid(),
                CustomerId = ride.CustomerId.ToGuid(),
                ArrivalTime = ride.Stopped ?? DateTime.MinValue
            };
        }
    }
}
