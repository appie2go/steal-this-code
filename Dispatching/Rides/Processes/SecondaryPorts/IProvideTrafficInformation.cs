using System;
using System.Threading.Tasks;

namespace Dispatching.Rides.Processes.SecondaryPorts
{
    public interface IProvideTrafficInformation
    {
        Task<Kilometer> GetDistanceBetweenLocations(Location a, Location b);

        Task<DateTime> GetTimeOfArival(DateTime departureTime, Kilometer distanceToCover);
    }
}
