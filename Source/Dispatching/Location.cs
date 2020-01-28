namespace Dispatching
{
    public struct Location
    {
        public decimal Longitude { get; }
        public decimal Latitude { get; }

        public Location(decimal longitude, decimal latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }


#region Equality

        public static bool operator ==(Location left, Location right)
        {
            return left.Longitude == right.Longitude && left.Latitude == right.Latitude;
        }

        public static bool operator !=(Location left, Location right)
        {
            return !(left == right);
        }

        public bool Equals(Location other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return this == (Location)obj;
        }

        public override int GetHashCode()
        {
            return Longitude.GetHashCode() * Latitude.GetHashCode() * 399;
        }

#endregion
    }
}
