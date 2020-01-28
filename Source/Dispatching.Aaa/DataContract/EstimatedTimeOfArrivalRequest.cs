using System;

namespace Dispatching.Aaa.DataContract
{
    public class EstimatedTimeOfArrivalRequest
    {
        public int Kilometers { get; set; }

        public DateTime TimeOfDeparture { get; set; }
    }
}
