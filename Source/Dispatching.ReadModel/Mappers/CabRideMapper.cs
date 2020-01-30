using Dispatching.ReadModel.PersistenceModel;

namespace Dispatching.ReadModel.Mappers
{
    internal class CabRideMapper : IApply<CabRide>
    {
        public void Apply(CabRide current, CabRide updated)
        {
            current.Id = updated.Id;
            current.CustomerId = updated.CustomerId;
            current.TimeOfArrival = updated.TimeOfArrival;
        }
    }
}
