using System.Net.Http;

namespace Dispatching.Aaa.Mapping
{
    public interface IGetDistanceRequestMapper
    {
        HttpContent Map(Location a, Location b);
    }
}
