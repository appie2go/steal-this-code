using System.Threading.Tasks;

namespace Dispatching.Rides.Processes.SecondaryPorts
{
    public interface IProvideLocation
    {
        Task<Location> GetTrainStationLocation();
    }
}
