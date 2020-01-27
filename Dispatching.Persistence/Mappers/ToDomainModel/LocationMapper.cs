namespace Dispatching.Persistence.Mappers.ToDomainModel
{
    internal class LocationMapper : IMapToDomainModel<PersistenceModel.Location, Location>
    {
        public Location Map(PersistenceModel.Location input)
        {
            return new Location(input.Longitude, input.Latitude);
        }
    }
}
