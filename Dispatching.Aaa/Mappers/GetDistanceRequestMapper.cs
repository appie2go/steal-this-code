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

            return new StringContent(JsonConvert.SerializeObject(request));
        }
    }
}
