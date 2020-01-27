namespace Dispatching.Aaa.DataContract
{
    public class GetDistanceRequest
    {
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public decimal DestinationLongitude { get; set; }
        public decimal DestinationLatitude { get; set; }

    }
}
