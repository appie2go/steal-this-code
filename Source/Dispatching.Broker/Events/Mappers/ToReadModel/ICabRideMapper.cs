using Dispatching.ReadModel.PersistenceModel;

namespace Dispatching.Broker.Events.Mappers.ToReadModel
{
    public interface ICabRideMapper
    {
        CabRide Map(DroveCustomerToTrainStation @event);
    }
}
