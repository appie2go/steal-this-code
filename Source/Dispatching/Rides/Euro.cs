using System;
using System.Diagnostics;

namespace Dispatching.Rides
{
    [DebuggerDisplay("{_amount:C}")]
    public struct Euro : IEquatable<Euro>
    {
        public decimal _amount { get; }
        
        public static Euro None => new Euro(0);
        
        public static Euro FromDecimal(decimal amount) => new Euro(amount);

        private Euro(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Amount must be bigger than 0.");
            }

            _amount = amount;
        }

#region Equality

        public static bool operator ==(Euro left, Euro right) => left.Equals(right);

        public static bool operator !=(Euro left, Euro right) => !(left == right);

        public bool Equals(Euro other) => this.Equals((object)other);

        public override int GetHashCode()
        {
            return _amount.GetHashCode() * 397;
        }

#endregion

        public static Euro operator *(Euro amount, decimal multiplier)
        {
            var result = amount._amount * multiplier;
            return new Euro(result);
        }

        public static Euro operator /(Euro amount, decimal devideBy)
        {
            var result = amount._amount / devideBy;
            return new Euro(result);
        }

        public static Euro operator *(Euro amount, double multiplier)
        {
            return amount * (decimal)multiplier;
        }

        public static Euro operator /(Euro amount, double devideBy)
        {
            return amount / (decimal)devideBy;
        }

        public decimal ToDecimal()
        {
            return _amount;
        }
        
        public override string ToString()
        {
            return $"{_amount:C}";
        }
    }
}