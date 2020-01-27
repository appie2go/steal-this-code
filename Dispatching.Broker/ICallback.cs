using System;
using System.Threading.Tasks;

namespace Dispatching.Broker
{
    public interface ICallback
    {
        Task CallBack<T>(Guid id);
    }
}
