using AutoFixture;
using Dispatching.Aaa;
using Dispatching.Specifications.TestContext;
using NSubstitute;
using System;
using System.Net;
using System.Net.Http;

namespace Dispatching.Specifications.TestCases.Aaa
{
    internal class EstimatedTimeOfArrival : TestCase<IHttpClient>
    {
        private readonly Fixture _fixture = new Fixture();

        private DateTime _estimatedTimeOfArrival;
        private HttpStatusCode _statusCode;

        public EstimatedTimeOfArrival()
        {
            _estimatedTimeOfArrival = _fixture.Create<DateTime>();
            _statusCode = HttpStatusCode.OK;
        }

        public EstimatedTimeOfArrival WithEstimatedTimeOrArrival(DateTime timeOfArrival)
        {
            _estimatedTimeOfArrival = timeOfArrival;
            return this;
        }

        protected override void Apply(IHttpClient substitute)
        {
            const string endpointAddress = "http://api.aaa.com/api/eta";

            var response = _fixture.Create<Response>();
            response.Eta = _estimatedTimeOfArrival;
            substitute
                .PostAsync(Arg.Is(endpointAddress), Arg.Any<HttpContent>())
                .Returns(response.ToHttpResponseMessage(_statusCode));
        }

        public class Response
        {
            public DateTime Eta { get; set; }

            public int TrafficIntensity { get; set; }
        }
    }   
}
