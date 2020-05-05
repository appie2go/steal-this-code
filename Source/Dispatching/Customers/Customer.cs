using DomainDrivenDesign.DomainObjects;

namespace Dispatching.Customers
{
    public class Customer : Entity<Customer>
    {
        public Customer(Id<Customer> id) : base(id) { }

        /* 
         * Todo: Implement things customers do here
         * I noticed it's pretty convinient to make folders with 1 or 2 entities in them
         *  and put all the value types that are only used for this entity in the same folder.
         * Refer to /Rides/Ride.cs for an example of that.
         */
    }
}
