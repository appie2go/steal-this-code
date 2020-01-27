using Dispatching.Broker;
using Dispatching.ReadModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Dispatching.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CabRideController : ControllerBase
    {
        private readonly ICabRideRepository _cabRideRepository;
        private readonly IQueue _queue;

        public CabRideController(ICabRideRepository cabRideRepository, IQueue queue)
        {
            _cabRideRepository = cabRideRepository;
            _queue = queue;
        }

        [HttpGet]
        public async Task<ActionResult> Get(Guid id)
        {
            var result = await _cabRideRepository.FindById(id);
            return Ok(new
            {
                CabRide = result
            });
        }
        
        [HttpPost]
        public void Post([FromBody] Broker.Commands.DriveCustomerToTrainStation command)
        {
            _queue.Enqueue(command);
        }
    }
}
