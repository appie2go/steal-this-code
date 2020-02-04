using AutoFixture;
using Dispatching.Api.Controllers;
using Dispatching.Broker.Commands;
using Dispatching.Specifications.TestCases.Aaa;
using Dispatching.Specifications.TestCases.Database;
using Dispatching.Specifications.TestContext;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using System;
using System.Linq;
using FluentAssertions;

namespace Dispatching.Specifications.Specs
{
    [Binding]
    [Scope(Feature = "Rides")]
    public class DispatchingSteps
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly ContextBuilder _contextBuilder;
        private Context _context;

        private DriveCustomerToTrainStation _command;

        private Location _customerLocation;
        private Location _trainstationLocation;
        private Location _cabLocation;

        private DateTime _currentTime;

        internal DispatchingSteps(ContextBuilder contextBuilder)
        {
            _contextBuilder = contextBuilder;
        }

        [BeforeScenario]
        public void Initialize()
        {
            // Set the current time
            _currentTime = _fixture.Create<DateTime>();
            _contextBuilder.SetTime(_currentTime);

            // Populate the database
            _customerLocation = _fixture.Create<Location>();
            _trainstationLocation = _fixture.Create<Location>();
            _cabLocation = _fixture.Create<Location>();

            _contextBuilder.With(new Distance(_customerLocation, _cabLocation));
            _contextBuilder.With(new Distance(_customerLocation, _trainstationLocation));
            _contextBuilder.With(new Distance(_trainstationLocation, _cabLocation));
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
            var eta = _currentTime.AddMinutes(_fixture.Create<int>());

            var testCase = new EstimatedTimeOfArrival()
                .WithEstimatedTimeOrArrival(eta)
                .AppendWith(new EstimatedDistance());

            _contextBuilder.With(testCase);
        }

        [When("the customer has been driven to the trainstation")]
        public async Task Drive()
        {
            _context = _contextBuilder.Build();
            await _context.Invoke<CabRideController>((x) => x.Post(_command));
        }

        [Then("the cab it's new location is the trainstation")]
        public void AssertCabsLocationHasBeenUpdated()
        {
            using (var dbContext = _context.GetWriteDbContext())
            {
                var cab = dbContext.Cabs.Single();
                cab.Latitude.Should().Be(_trainstationLocation.Latitude);
                cab.Longitude.Should().Be(_trainstationLocation.Longitude);
            }
        }

        [Then("the ride has been registered")]
        public void AssertRideAvailable()
        {
            using (var dbContext = _context.GetReadDbContext())
            {
                dbContext.CabRides.Count().Should().Be(1);
            }
        }
    }
}
