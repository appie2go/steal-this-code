using Dispatching.Customers;
using DomainDrivenDesign.DomainObjects;
using System.Threading.Tasks;

namespace Dispatching.Rides.Processes.PrimaryPorts
{
    public interface ICabRideService
    {
        Task<Ride> BringCustomerToTheTrainStation(Id<Customer> customerId, Location customerLocation);
    }
}
