using Dispatching.Aaa.DataContract;
using Dispatching.Rides;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace Dispatching.Aaa.Mappers
{
    public class EstimatedTimeOfArrivalRequestMapper : IEstimatedTimeOfArrivalRequestMapper
    {
        public HttpContent Map(DateTime departureTime, Kilometer distance)
        {
            var request = new EstimatedTimeOfArrivalRequest
            {
                TimeOfDeparture = departureTime,
                Kilometers = (int)distance.ToDecimal()
            };

            return new StringContent(JsonConvert.SerializeObject(request));
        }
    }
}
