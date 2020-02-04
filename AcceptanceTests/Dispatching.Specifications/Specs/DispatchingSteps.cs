using AutoFixture;
using Dispatching.Api.Controllers;
using Dispatching.Broker.Commands;
using Dispatching.Specifications.TestCases.Aaa;
using Dispatching.Specifications.TestCases.Database;
using Dispatching.Specifications.TestContext;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;
using Rebus.ServiceProvider;

namespace Dispatching.Specifications.Specs
{
    [Binding]
    [Scope(Feature = "Rides")]
    public class DispatchingSteps
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly ContextBuilder _contextBuilder;

        private DriveCustomerToTrainStation _command;

        private Location _customerLocation;
        private Location _trainstationLocation;
        private Location _cabLocation;

        internal DispatchingSteps(ContextBuilder contextBuilder)
        {
            _contextBuilder = contextBuilder;
        }

        [BeforeScenario]
        public void Initialize()
        {
            _customerLocation = _fixture.Create<Location>();
            _trainstationLocation = _fixture.Create<Location>();
            _cabLocation = _fixture.Create<Location>();
        }

        [Given("a cab")]
        public void CreateCab()
        {
            _contextBuilder.With(new AnyRandomCab().At(_cabLocation));
        }

        [Given("a train station called \"(.*)\"")]
        public void CreateCustomer(string trainStationName)
        {
            _contextBuilder.With(new AnyRandomTrainStation().At(_trainstationLocation).WithName(trainStationName));
        }

        [Given("a customer who wants to go to Utrecht Centraal")]
        public void CreateCustomer()
        {
            _command = _fixture.Create<DriveCustomerToTrainStation>();
            _command.CurrentLatitude = _customerLocation.Latitude;
            _command.CurrentLongitude = _customerLocation.Longitude;
        }

        [Given("traffic information")]
        public void CreateTrafficInfromation()
        {
            var testCase = new EstimatedTimeOfArrival()
                .AppendWith(new EstimatedDistance());

            _contextBuilder.With(testCase);
        }

        [When("the customer has been driven to the trainstation")]
        public async Task Drive()
        {
            _contextBuilder.With(new Distance(_customerLocation, _cabLocation));
            _contextBuilder.With(new Distance(_customerLocation, _trainstationLocation));
            _contextBuilder.With(new Distance(_trainstationLocation, _cabLocation));

            var serviceCollection = _contextBuilder.Create();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            serviceProvider.UseRebus();

            var controller = serviceProvider.GetService<CabRideController>();
            await controller.Post(_command);

            await Task.Delay(5000); // Allow commands to be processed

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
