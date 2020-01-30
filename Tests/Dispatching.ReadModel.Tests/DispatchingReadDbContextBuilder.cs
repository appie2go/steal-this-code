using AutoFixture;
using Dispatching.Persistence.Tests;
using Dispatching.ReadModel.PersistenceModel;

namespace Dispatching.ReadModel.Tests
{
    internal class DispatchingReadDbContextBuilder
    {
        private readonly Fixture _fixture = new Fixture();

        private DispatchingReadDbContext _dbContext;
        
        public DispatchingReadDbContextBuilder(string databaseName)
        {
            _dbContext = new InMemoryDispatchingDbContext(databaseName);
        }

        public DispatchingReadDbContextBuilder WithCabRide(CabRide ride)
        {
            _dbContext.CabRides.Add(ride);
            return this;
        }

        public DispatchingReadDbContext Build()
        {
            _dbContext.SaveChanges();
            return _dbContext;
        }
    }
}
