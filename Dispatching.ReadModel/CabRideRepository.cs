using Dispatching.ReadModel.PersistenceModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Dispatching.ReadModel
{
    public interface ICabRideRepository
    {
        Task<IEnumerable<CabRide>> GetAll();
        Task Save(CabRide input);
        Task<CabRide> FindById(Guid id);
    }

    internal class CabRideRepository : Repository<CabRide>, ICabRideRepository
    {
        private readonly DispatchingReadDbContext _dispatchingReadContext;

        public CabRideRepository(DispatchingReadDbContext dispatchingReadContext) : base(dispatchingReadContext)
        {
            _dispatchingReadContext = dispatchingReadContext;
        }

        public async Task<IEnumerable<CabRide>> GetAll()
        {
            return await _dispatchingReadContext.CabRides.ToListAsync();
        }

        protected override async Task Add(CabRide newItem)
        {
            await _dispatchingReadContext.CabRides.AddAsync(newItem);
        }

        protected override void CopyValues(CabRide currentItem, CabRide updatedItem)
        {
            currentItem.Id = updatedItem.Id;
            currentItem.CustomerId = updatedItem.CustomerId;
            currentItem.TimeOfArrival = updatedItem.TimeOfArrival;
        }

        public override async Task<CabRide> FindById(Guid id)
        {
            return await _dispatchingReadContext.CabRides
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
