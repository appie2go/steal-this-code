using Dispatching.Broker.Commands;
using Dispatching.Rides;
using System;

namespace Dispatching.Broker.Events.Mappers.ToDomainModel
{
    public interface ICabRideMapper
    {
        DroveCustomerToTrainStation MapSuccessEvent(Ride ride);
        DriveCustomerToTrainStationFailed MapFailedEvent(DriveCustomerToTrainStation command, Exception error);
    }
}
