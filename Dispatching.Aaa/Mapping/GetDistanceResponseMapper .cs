using Dispatching.Aaa.DataContract;
using Dispatching.Rides;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace Dispatching.Aaa.Mapping
{
    internal class GetDistanceResponseMapper : IGetDistanceResponseMapper
    {
        public Kilometer Map(HttpResponseMessage responseMessage)
        {
            if (responseMessage.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new ApplicationException($"Failed to get data from AAA service: {responseMessage.ReasonPhrase}");
            }

            var response = responseMessage.Content.ReadAsStringAsync()
                .GetAwaiter()
                .GetResult();

            var distanceResponse = JsonConvert.DeserializeObject<GetDistanceResponse>(response);
            return Kilometer.FromDecimal(distanceResponse.Kilometers);
        }
    }
}
