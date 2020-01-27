using System;
using System.ComponentModel.DataAnnotations;

namespace Dispatching.Persistence.PersistenceModel
{
    public class Cab
    {
        [Key]
        public Guid Id { get; set; }

        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
    }
}
