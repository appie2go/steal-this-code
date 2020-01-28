using Dispatching.Aaa.DataContract;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace Dispatching.Aaa.Mappers
{
    public class EstimatedTimeOfArrivalResponseMapper : IEstimatedTimeOfArrivalResponseMapper
    {
        public DateTime Map(HttpResponseMessage responseMessage)
        {
            if (responseMessage.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new ApplicationException($"Failed to get data from AAA service: {responseMessage.ReasonPhrase}");
            }

            var response = responseMessage.Content.ReadAsStringAsync()
                .GetAwaiter()
                .GetResult();

            var estimatedTimeOfArrivalResponse = JsonConvert.DeserializeObject<EstimatedTimeOfArrivalResponse>(response);
            return estimatedTimeOfArrivalResponse.Eta;
        }
    }
}
