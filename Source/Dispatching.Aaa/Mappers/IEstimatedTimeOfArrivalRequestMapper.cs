using Dispatching.Aaa.DataContract;
using Dispatching.Rides;
using System;
using System.Net.Http;

namespace Dispatching.Aaa.Mappers
{
    public interface IEstimatedTimeOfArrivalRequestMapper
    {
        HttpContent Map(DateTime departureTime, Kilometer distance);
    }
}
