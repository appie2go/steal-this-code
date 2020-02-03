using AutoFixture;
using Dispatching.Api.Controllers;
using Dispatching.Broker;
using Dispatching.Broker.Commands;
using Dispatching.Broker.Handlers;
using Dispatching.Rides.Processes.PrimaryPorts;
using Dispatching.Rides.Processes.SecondaryPorts;
using Dispatching.Specifications.TestCases;
using Dispatching.Specifications.TestContext;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Persistence.InMem;
using Rebus.Routing.TypeBased;
using Rebus.Transport.InMem;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Dispatching.Specifications.Specs
{
    [Binding]
    [Scope(Feature= "Rides")]
    public class DispatchingSteps
    {
        private readonly ContextBuilder _contextBuilder;

        private IServiceProvider _serviceProvider;
        private DriveCustomerToTrainStation _command;

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
            var activator = new BuiltinHandlerActivator();
            activator.Register((x) => (DriveCustomerToTrainStationHandler)_serviceProvider.GetService(typeof(DriveCustomerToTrainStationHandler)));

            Configure.With(activator)
                    .Transport(t => t.UseInMemoryTransport(new InMemNetwork(), "Test"))
                    .Subscriptions(s => s.StoreInMemory())
                    .Routing(r => r.TypeBased().MapAssemblyOf<IQueue>("Test"))
                    .Start();

            // Create the serviceprovider
            _serviceProvider = _contextBuilder
                .Replace(activator.Bus)
                .Create();

            // Invoke the application
            var controller = (CabRideController)_serviceProvider.GetService(typeof(CabRideController));
            await controller.Post(_command);

            // Allow async stuff to complete
            await Task.Delay(200);
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
