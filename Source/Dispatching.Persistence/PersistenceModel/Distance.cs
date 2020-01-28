using System;
using System.ComponentModel.DataAnnotations;

namespace Dispatching.Persistence.PersistenceModel
{
    public class Distance
    {
        [Key]
        public Guid Id { get; set; }

        public decimal Kilometers { get; set; }

        public decimal ToLongitude { get; set; }
        public decimal FromLongitude { get; set; }

        public decimal ToLatitude { get; set; }
        public decimal FromLatitude { get; set; }
    }
}
