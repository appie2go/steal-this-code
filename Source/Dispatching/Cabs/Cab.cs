using Dispatching.Customers;
using DomainDrivenDesign.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dispatching.Cabs
{
    public class Cab : Entity<Cab>
    {
        private readonly IList<Id<Customer>> _customers = new List<Id<Customer>>();

        public IEnumerable<Id<Customer>> Passengers => _customers;
        public Location CurrentLocation { get; private set; }

        public Cab(Id<Cab> id, Location currentLocation) : base(id) 
        {
            CurrentLocation = currentLocation;
        }

        public void GoTo(Location location)
        {
            if (CurrentLocation == location) 
            {
                throw new ApplicationException("Already there...");
            }

            CurrentLocation = location;
        }

        public void Embarc(Id<Customer> customer)
        {
            if (_customers.Count() > 3) 
            {
                throw new ApplicationException("The cab is full.");
            }

            if (_customers.Contains(customer))
            {
                throw new ApplicationException("This person is already in the cab.");
            }

            _customers.Add(customer);
        }
    }
}
