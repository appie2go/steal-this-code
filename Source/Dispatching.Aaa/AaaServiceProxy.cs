using Dispatching.Aaa.Mappers;
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
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _estimatedTimeOfArrivalRequestMapper = estimatedTimeOfArrivalRequestMapper ?? throw new ArgumentNullException(nameof(estimatedTimeOfArrivalRequestMapper));
            _estimatedTimeOfArrivalResponseMapper = estimatedTimeOfArrivalResponseMapper ?? throw new ArgumentNullException(nameof(estimatedTimeOfArrivalResponseMapper));
            _getDistanceRequestMapper = getDistanceRequestMapper ?? throw new ArgumentNullException(nameof(getDistanceRequestMapper));
            _getDistanceResponseMapper = getDistanceResponseMapper ?? throw new ArgumentNullException(nameof(getDistanceResponseMapper));
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
