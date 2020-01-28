using System;

namespace Dispatching.Broker.Commands
{
    public class DriveCustomerToTrainStation
    {
        public Guid CustomerId { get; set; }

        public decimal CurrentLongitude { get; set; }

        public decimal CurrentLatitude { get; set; }
    }
}
