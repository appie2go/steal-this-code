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

        private HttpStatusCode _statusCode;

        public EstimatedTimeOfArrival()
        {
            _statusCode = HttpStatusCode.OK;
        }

        protected override void Apply(IHttpClient substitute)
        {
            const string endpointAddress = "http://api.aaa.com/api/eta";

            var response = _fixture.Create<Response>();
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
