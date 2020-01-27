using Dispatching.Rides;
using System.Net.Http;

namespace Dispatching.Aaa.Mapping
{
    public interface IGetDistanceResponseMapper
    {
        Kilometer Map(HttpResponseMessage responseMessage);
    }
}
