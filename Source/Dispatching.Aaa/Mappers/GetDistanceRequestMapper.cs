using Dispatching.Aaa.DataContract;
using Newtonsoft.Json;
using System.Net.Http;

namespace Dispatching.Aaa.Mappers
{
    internal class GetDistanceRequestMapper : IGetDistanceRequestMapper
    {
        public HttpContent Map(Location a, Location b)
        {
            var request = new GetDistanceRequest
            {
                Latitude = a.Latitude,
                Longitude = a.Longitude,
                DestinationLatitude = b.Latitude,
                DestinationLongitude = b.Longitude,
            };

            var jsonString = JsonConvert.SerializeObject(request);
            return new StringContent(jsonString, Encoding.UTF8, "application/json");
        }
    }
}
