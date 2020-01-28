using System;
using System.Diagnostics;

namespace Dispatching.Rides
{
    [DebuggerDisplay("{_amount} km")]
    public struct Kilometer : IEquatable<Kilometer>
    {
        public decimal _distance { get; }

        public static Kilometer None => new Kilometer(0);

        public static Kilometer FromDecimal(decimal kilometers) => new Kilometer(kilometers);

        private Kilometer(decimal distance)
        {
            if (distance < 0)
            {
                throw new ArgumentException("Distance must be bigger than 0 km.", nameof(distance));
            }

            _distance = distance;
        }

        #region Equality

        public static bool operator ==(Kilometer left, Kilometer right)
        {
            return left._distance == right._distance;
        }

        public static bool operator !=(Kilometer left, Kilometer right)
        {
            return !(left == right);
        }

        public bool Equals(Kilometer other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return this == (Kilometer)obj;
        }

        public override int GetHashCode()
        {
            return _distance.GetHashCode() * 398;
        }

        #endregion

        public static Kilometer operator + (Kilometer left, Kilometer right)
        {
            var result = left._distance + right._distance;
            return FromDecimal(result);
        }

        public static Kilometer operator - (Kilometer left, Kilometer right)
        {
            var result = left._distance - right._distance;
            return FromDecimal(result);
        }

        public decimal ToDecimal() 
        {
            return _distance;
        }

        public override string ToString()
        {
            return $"{_distance} km";
        }
    }
}
