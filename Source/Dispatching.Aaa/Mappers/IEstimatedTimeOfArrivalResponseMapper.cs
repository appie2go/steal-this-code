using Dispatching.Aaa.DataContract;
using System;
using System.Net.Http;

namespace Dispatching.Aaa.Mappers
{
    public interface IEstimatedTimeOfArrivalResponseMapper
    {
        DateTime Map(HttpResponseMessage estimatedTimeOfArrivalResponse);
    }
}
