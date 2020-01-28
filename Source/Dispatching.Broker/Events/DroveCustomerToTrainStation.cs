using System;

namespace Dispatching.Broker.Events
{
    public class DroveCustomerToTrainStation
    {
        public Guid CabRideId { get; set; }

        public Guid CustomerId { get; set; }
        public DateTime ArrivalTime { get; set; }
    }
}
