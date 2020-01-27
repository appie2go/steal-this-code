using Dispatching.Aaa.Mapping;
using Dispatching.Rides;
using Dispatching.Rides.Processes.SecondaryPorts;
using System;
using System.Threading.Tasks;

namespace Dispatching.Aaa
{
    internal class AaaServiceProxy : IProvideTrafficInformation
    {
        private const string etaEndpointAddress = "http://api.aaa.com/api/eta"; // Todo: get this imaginary endpoint-address form config
        private const string distanceEndpointAddress = "http://api.aaa.com/api/distance"; // Todo: get this imaginary endpoint-address form config

        private readonly IHttpClient _httpClient;
        private readonly IEstimatedTimeOfArrivalRequestMapper _estimatedTimeOfArrivalRequestMapper;
        private readonly IEstimatedTimeOfArrivalResponseMapper _estimatedTimeOfArrivalResponseMapper;
        private readonly IGetDistanceRequestMapper _getDistanceRequestMapper;
        private readonly IGetDistanceResponseMapper _getDistanceResponseMapper;

        public AaaServiceProxy(IHttpClient httpClient,
            IEstimatedTimeOfArrivalRequestMapper estimatedTimeOfArrivalRequestMapper,
            IEstimatedTimeOfArrivalResponseMapper estimatedTimeOfArrivalResponseMapper,
            IGetDistanceRequestMapper getDistanceRequestMapper,
            IGetDistanceResponseMapper getDistanceResponseMapper)
        {
            _httpClient = httpClient;
            _estimatedTimeOfArrivalRequestMapper = estimatedTimeOfArrivalRequestMapper;
            _estimatedTimeOfArrivalResponseMapper = estimatedTimeOfArrivalResponseMapper;
            _getDistanceRequestMapper = getDistanceRequestMapper;
            _getDistanceResponseMapper = getDistanceResponseMapper;
        }

        public async Task<Kilometer> GetDistanceBetweenLocations(Location a, Location b)
        {
            var request = _getDistanceRequestMapper.Map(a, b);
            var response = await _httpClient.PostAsync(distanceEndpointAddress, request);
            return _getDistanceResponseMapper.Map(response);
        }

        public async Task<DateTime> GetTimeOfArival(DateTime departureTime, Kilometer distanceToCover)
        {            
            var request = _estimatedTimeOfArrivalRequestMapper.Map(departureTime, distanceToCover);
            var response = await _httpClient.PostAsync(etaEndpointAddress, request);
            return _estimatedTimeOfArrivalResponseMapper.Map(response);
        }
    }
}
