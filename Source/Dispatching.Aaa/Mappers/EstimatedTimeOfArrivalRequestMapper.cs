using Dispatching.Aaa.DataContract;
using Dispatching.Rides;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace Dispatching.Aaa.Mappers
{
    internal class EstimatedTimeOfArrivalRequestMapper : IEstimatedTimeOfArrivalRequestMapper
    {
        public HttpContent Map(DateTime departureTime, Kilometer distance)
        {
            var request = new EstimatedTimeOfArrivalRequest
            {
                TimeOfDeparture = departureTime,
                Kilometers = (int)distance.ToDecimal()
            };

            var jsonString = JsonConvert.SerializeObject(request);
            return new StringContent(jsonString, Encoding.UTF8, "application/json");
        }
    }
}
