using Dispatching.Broker.Events;
using Dispatching.Broker.Events.Mappers.ToReadModel;
using Dispatching.ReadModel;
using Rebus.Handlers;
using System;
using System.Threading.Tasks;

namespace Dispatching.Broker.Handlers
{
    public class DroveCustomerToTrainStationHandler : IHandleMessages<DroveCustomerToTrainStation>
    {
        private readonly ICallback _callback;
        private readonly ICabRideRepository _cabRideRepository;
        private readonly ICabRideMapper _cabRideMapper;

        public DroveCustomerToTrainStationHandler(ICallback callback,
            ICabRideRepository cabRideRepository,
            ICabRideMapper cabRideMapper)
        {
            _callback = callback ?? throw new ArgumentNullException(nameof(callback));
            _cabRideRepository = cabRideRepository ?? throw new ArgumentNullException(nameof(cabRideRepository));
            _cabRideMapper = cabRideMapper ?? throw new ArgumentNullException(nameof(cabRideMapper));
        }

        public async Task Handle(DroveCustomerToTrainStation message)
        {
            await UpdateReadModel(message);

            await _callback.CallBack<DroveCustomerToTrainStation>(message.CabRideId);
        }

        private async Task UpdateReadModel(DroveCustomerToTrainStation message) 
        {
            var cabRide = _cabRideMapper.Map(message);
            await _cabRideRepository.Save(cabRide);
        }
    }
}
