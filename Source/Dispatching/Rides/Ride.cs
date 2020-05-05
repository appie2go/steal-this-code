using Dispatching.Cabs;
using Dispatching.Customers;
using DomainDrivenDesign.DomainObjects;
using System;

namespace Dispatching.Rides
{
    public class Ride : Entity<Ride>
    {
        private readonly Euro PricePerMinute = Euro.FromDecimal(3.5m);

        public Id<Customer> CustomerId { get; }

        public Id<Cab> CabId { get; }

        public DateTime? Started { get; private set; }

        public DateTime? Stopped { get; private set; }

        public Location? Destination { get; private set; }

        public Euro Price 
        {
            get 
            {
                if (Stopped == null || Started == null)
                {
                    return Euro.None;
                }

                var elapsed = Stopped.Value - Started.Value;
                return PricePerMinute * elapsed.TotalMinutes;
            }
        }

        public Ride(Id<Ride> id, Id<Customer> customerId, Id<Cab> cabId) : base(id)
        {
            CustomerId = customerId ?? throw new ArgumentNullException(nameof(customerId));
            CabId = cabId ?? throw new ArgumentNullException(nameof(cabId));
        }

        public void SetDestination(Location destination)
        {
            Destination = destination;
        }

        public void Start(DateTime whenTheRideStarts)
        {
            // Protect your business invariants!
            if (Stopped.HasValue)
            {
                throw new ApplicationException("The ride cannot start after it has ended.");
            }

            if (Destination == null) 
            {
                throw new ApplicationException("Can't start the ride if I don't know where to go! Provide a destination first!");
            }

            Started = whenTheRideStarts;
        }

        public void Stop(DateTime whenTheRideStops)
        {
            // Protect your business invariants!
            if (!Started.HasValue)
            {
                throw new ApplicationException("Cannot stop a ride which hasn't started..");
            }

            if (Started.Value > whenTheRideStops) 
            {
                throw new ApplicationException("A ride cannot end earlier than it started.");
            }

            Stopped = whenTheRideStops;
        }
    }
}
