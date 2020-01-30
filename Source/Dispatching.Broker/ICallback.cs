using System;
using System.Threading.Tasks;

namespace Dispatching.Broker
{
    public interface ICallback
    {
        Task CallBack(Guid id);
    }
}
