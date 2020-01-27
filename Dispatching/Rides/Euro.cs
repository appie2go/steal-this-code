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
            _amount = amount;
        }

#region Equality

        public static bool operator ==(Euro left, Euro right)
        {
            return left._amount == right._amount;
        }

        public static bool operator !=(Euro left, Euro right)
        {
            return !(left == right);
        }

        public bool Equals(Euro other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return this == (Euro)obj;
        }

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

        public override string ToString()
        {
            return $"{_amount:C}";
        }
    }
}