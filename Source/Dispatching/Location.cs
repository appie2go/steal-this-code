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

        public static bool operator ==(Location left, Location right) => left.Equals(right);

        public static bool operator !=(Location left, Location right) => !(left == right);

        public bool Equals(Location other) => this.Equals((object)other);

        public override int GetHashCode()
        {
            return Longitude.GetHashCode() * Latitude.GetHashCode() * 399;
        }

#endregion
    }
}
