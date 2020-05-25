using Dispatching.Customers;
using DomainDrivenDesign.DomainObjects;

namespace Dispatching.Broker.Commands.Mappers
{
    internal class DriveCustomerToTrainStationMapper : IDriveCustomerToTrainStationMapper
    {
        public Id<Customer> MapToCustomerId(DriveCustomerToTrainStation driveCustomerToTrainStation)
        {
            return Id<Customer>.Create(driveCustomerToTrainStation.CustomerId);
        }

        public Location MapToCustomerLocation(DriveCustomerToTrainStation driveCustomerToTrainStation)
        {
            var @long = driveCustomerToTrainStation.CurrentLongitude;
            var lat = driveCustomerToTrainStation.CurrentLatitude;
            return new Location(@long, lat);
        }
    }
}
