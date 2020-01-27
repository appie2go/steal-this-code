using Dispatching.Customers;
using Dispatching.Framework;

namespace Dispatching.Broker.Commands.Mappers
{
    public interface IDriveCustomerToTrainStationMapper
    {
        Id<Customer> MapToCustomerId(DriveCustomerToTrainStation driveCustomerToTrainStation);
        Location MapToCustomerLocation(DriveCustomerToTrainStation driveCustomerToTrainStation);
    }
}
