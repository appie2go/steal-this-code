using Dispatching.Rides;
using System.Net.Http;

namespace Dispatching.Aaa.Mappers
{
    public interface IGetDistanceResponseMapper
    {
        Kilometer Map(HttpResponseMessage responseMessage);
    }
}
