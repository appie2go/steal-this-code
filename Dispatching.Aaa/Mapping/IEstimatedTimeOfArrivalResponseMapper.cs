using Dispatching.Aaa.DataContract;
using System;
using System.Net.Http;

namespace Dispatching.Aaa.Mapping
{
    public interface IEstimatedTimeOfArrivalResponseMapper
    {
        DateTime Map(HttpResponseMessage estimatedTimeOfArrivalResponse);
    }
}
