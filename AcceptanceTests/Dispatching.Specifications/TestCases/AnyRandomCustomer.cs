using Dispatching.Persistence;
using Dispatching.ReadModel;
using Dispatching.Specifications.TestContext;
using System;

namespace Dispatching.Specifications.TestCases
{
    internal class AnyRandomCustomer : TestCase
    {
        private Guid _id;

        public AnyRandomCustomer WithId(Guid id)
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
