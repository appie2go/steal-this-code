using Dispatching.ReadModel.PersistenceModel;

namespace Dispatching.Broker.Events.Mappers.ReadModel
{
    public interface ICabRideMapper
    {
        CabRide Map(DroveCustomerToTrainStation @event);
    }
}
