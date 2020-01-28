using Dispatching.Cabs;
using System.Threading.Tasks;

namespace Dispatching.Rides.Processes.SecondaryPorts
{
    public interface IProvideCab
    {
        Task<Cab> GetNearestAvailableCab(Location location);

        Task Update(Cab cab);
    }
}
