using Dispatching.Persistence;
using Dispatching.ReadModel;
using Dispatching.Specifications.TestContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dispatching.Specifications.TestCases
{
    internal class AnyRandomCab : TestCase
    {
        protected override void Apply(DispatchingDbContext dispatchingDbContext)
        {
            dispatchingDbContext.Cabs.Add(new Persistence.PersistenceModel.Cab
            {

            });

            dispatchingDbContext.SaveChanges();
        }

        protected override void Apply(DispatchingReadDbContext dispatchingDbContext)
        {

        }
    }
}
