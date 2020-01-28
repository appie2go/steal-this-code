using System;

namespace Dispatching.Framework
{
    public abstract class Entity<T> where T : Entity<T>
    {
        public Id<T> Id { get; }

        protected Entity(Id<T> id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id), "Entity must have an id.");
        }

#region Equality
        
        public static bool operator ==(Entity<T> left, Entity<T> right)
        {
            return left?.Id == right?.Id;
        }
        
        public static bool operator !=(Entity<T> left, Entity<T> right)
        {
            return !(left == right);
        }

        public bool Equals(Entity<T> other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return this == obj as Entity<T>;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

#endregion
    }
}