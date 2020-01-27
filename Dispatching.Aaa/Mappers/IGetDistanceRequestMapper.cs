using System.Net.Http;

namespace Dispatching.Aaa.Mappers
{
    public interface IGetDistanceRequestMapper
    {
        HttpContent Map(Location a, Location b);
    }
}
