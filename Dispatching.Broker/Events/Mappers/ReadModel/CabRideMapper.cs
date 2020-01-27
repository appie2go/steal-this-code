using Dispatching.ReadModel.PersistenceModel;
using System;

namespace Dispatching.Broker.Events.Mappers.ReadModel
{
    internal class CabRideMapper : ICabRideMapper
    {
        public CabRide Map(DroveCustomerToTrainStation @event)
        {
            if (@event == null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            return new CabRide
            {
                Id = @event.CabRideId,
                CustomerId = @event.CustomerId,
                TimeOfArrival = @event.ArrivalTime
            };
        }
    }
}
