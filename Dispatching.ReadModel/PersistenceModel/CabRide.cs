using System;

namespace Dispatching.ReadModel.PersistenceModel
{
    public class CabRide : Entity
    {
        public Guid CustomerId { get; set; }

        public DateTime TimeOfArrival { get; set; }
    }
}
