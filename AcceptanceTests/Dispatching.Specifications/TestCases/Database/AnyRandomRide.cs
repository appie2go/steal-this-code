using Dispatching.Persistence;
using Dispatching.ReadModel;
using Dispatching.Specifications.TestContext;
using System;

namespace Dispatching.Specifications.TestCases.Database
{
    internal class AnyRandomRide : TestCase
    {
        private Guid _id;

        public AnyRandomRide WithId(Guid id)
        {
            _id = id;
            return this;
        }

        protected override void Apply(DispatchingDbContext dispatchingDbContext)
        {

        }

        protected override void Apply(DispatchingReadDbContext dispatchingDbContext)
        {

        }
    }
}
