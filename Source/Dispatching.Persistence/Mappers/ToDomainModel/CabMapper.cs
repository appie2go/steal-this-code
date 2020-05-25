using Dispatching.Cabs;
using DomainDrivenDesign.DomainObjects;

namespace Dispatching.Persistence.Mappers.ToDomainModel
{
    internal class CabMapper : IMapToDomainModel<PersistenceModel.Cab, Cab>
    {
        public Cab Map(PersistenceModel.Cab input)
        {
            var id = Id<Cab>.Create(input.Id);
            var location = new Location(input.Longitude, input.Latitude);

            return new Cab(id, location);
        }
    }
}
