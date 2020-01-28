using System;
using System.Threading.Tasks;

namespace Dispatching.Rides.Processes.SecondaryPorts
{
    public interface IProvideTime
    {
        Task<DateTime> GetCurrentTime();
    }
}
