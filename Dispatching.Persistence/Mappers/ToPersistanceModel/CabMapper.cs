using Dispatching.Cabs;
using System;

namespace Dispatching.Persistence.Mappers.ToPersistanceModel
{
    internal class CabMapper : IMapToPersistenceModel<Cab, PersistenceModel.Cab>
    {
        public void Apply(Cab domainmodel, PersistenceModel.Cab data)
        {
            data.Latitude = domainmodel.CurrentLocation.Latitude;
            data.Longitude = domainmodel.CurrentLocation.Longitude;
        }

        public PersistenceModel.Cab CreateNew()
        {
            return new PersistenceModel.Cab { Id = Guid.NewGuid() };
        }
    }
}
