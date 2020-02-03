using AutoFixture;
using Dispatching.Api.Controllers;
using Dispatching.Broker.Commands;
using Dispatching.Specifications.TestCases;
using Dispatching.Specifications.TestContext;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Dispatching.Specifications.Specs
{
    [Binding]
    [Scope(Feature= "Rides")]
    public class DispatchingSteps
    {
        private readonly ContextBuilder _contextBuilder;

        private DriveCustomerToTrainStation _command;

        private TestMessageBroker _application;

        internal DispatchingSteps(ContextBuilder contextBuilder)
        {
            _contextBuilder = contextBuilder;
        }

        [Given("a cab")]
        public void CreateCab()
        {
            _contextBuilder.With<AnyRandomCab>();
        }

        [Given("a customer")]
        public void CreateCustomer()
        {
            var fixture = new Fixture();

            _command = fixture.Create<DriveCustomerToTrainStation>();
            _contextBuilder.With(new AnyRandomCustomer().WithId(_command.CustomerId));
        }

        [Given("traffic information")]
        public void CreateTrafficInfromation()
        {
            _contextBuilder.With(new TrafficInformation());
        }

        [When("the customer has been driven to the trainstation")]
        public async Task Drive()
        {
            using (_application = new TestMessageBroker(_contextBuilder.Create()))
            {
                var controller = _application.GetService<CabRideController>();
                await controller.Post(_command);

                await Task.Delay(100); // Allow commands to be processed
            }
        }

        [Then("the cab it's new location is the trainstation")]
        public void AssertCabsLocationHasBeenUpdated()
        { 
        
        }

        [Then("the ride has been registered")]
        public void AssertRideAvailable()
        { 
        
        }
    }
}
