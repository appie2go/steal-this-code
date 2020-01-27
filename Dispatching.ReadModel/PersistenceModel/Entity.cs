using System;
using System.ComponentModel.DataAnnotations;

namespace Dispatching.ReadModel.PersistenceModel
{
    public abstract class Entity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
