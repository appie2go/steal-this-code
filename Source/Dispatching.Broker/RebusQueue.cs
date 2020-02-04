using Rebus.Bus;
using System;
using System.Threading.Tasks;

namespace Dispatching.Broker
{
    internal class RebusQueue : IQueue
    {
        private readonly IBus _bus;

        public RebusQueue(IBus bus)
        {
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
        }

        public async Task Enqueue<T>(T payload)
        {
            await _bus.Send(payload);
        }
    }
}
