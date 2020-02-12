using AutoFixture;
using Dispatching.Aaa;
using Dispatching.Specifications.TestContext;
using NSubstitute;
using System.Net;
using System.Net.Http;

namespace Dispatching.Specifications.TestCases.Aaa
{
    internal class EstimatedDistance : TestCase<IHttpClient>
    {
        private readonly Fixture _fixture = new Fixture();

        private HttpStatusCode _statusCode;

        public EstimatedDistance()
        {
            _statusCode = HttpStatusCode.OK;
        }

        protected override void Apply(IHttpClient substitute)
        {
            var endpointAddress = "http://api.aaa.com/api/distance";

            var response = _fixture.Create<Response>();
            substitute
                .PostAsync(Arg.Is(endpointAddress), Arg.Any<HttpContent>())
                .Returns(response.ToHttpResponseMessage(_statusCode));
        }

        public class Response
        {
            public int Kilometers { get; set; }
        }
    }
}
