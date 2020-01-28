
using System.Threading.Tasks;

namespace Dispatching.Broker
{
    public interface IQueue
    {
        Task Enqueue<T>(T payload);
    }
}
