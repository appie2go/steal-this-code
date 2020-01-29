using Dispatching.Broker.Commands;
using Dispatching.Rides;
using System;

namespace Dispatching.Broker.Events.Mappers.ToDomainModel
{
    internal class CabRideMapper : ICabRideMapper
    {
        public DriveCustomerToTrainStationFailed MapFailedEvent(DriveCustomerToTrainStation command, Exception exception)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            return new DriveCustomerToTrainStationFailed
            {
                Reason = exception.ToString()
            };
        }

        public DroveCustomerToTrainStation MapSuccessEvent(Ride ride)
        {
            if (ride == null)
            {
                throw new ArgumentNullException(nameof(ride));
            }

            return new DroveCustomerToTrainStation
            {
                CabRideId = ride.Id.ToGuid(),
                CustomerId = ride.CustomerId.ToGuid(),
                ArrivalTime = ride.Stopped ?? DateTime.MinValue
            };
        }
    }
}
