using Dispatching.Customers;
using DomainDrivenDesign.DomainObjects;

namespace Dispatching.Broker.Commands.Mappers
{
    public interface IDriveCustomerToTrainStationMapper
    {
        Id<Customer> MapToCustomerId(DriveCustomerToTrainStation driveCustomerToTrainStation);
        Location MapToCustomerLocation(DriveCustomerToTrainStation driveCustomerToTrainStation);
    }
}
