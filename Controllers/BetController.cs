using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using Orleans_BettingSite_Task.Grains;
using Orleans_BettingSite_Task.Interfaces;
using Orleans_BettingSite_Task.Requests;
using Orleans_BettingSite_Task.Responses;

namespace Orleans_BettingSite_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BetController : ControllerBase
    {
        private readonly IGrainFactory _factory;

        public BetController(IGrainFactory factory)
        {
            _factory = factory;
        }


        [HttpPost]
        public Task<ActionResult<BetCreateResponse>> CreateBet([FromBody] BetCreateRequest betRequest)
        {
            var intermediateGrain = _factory.GetGrain<IIntermediateGrain>(Guid.NewGuid());
            
            return Ok();
        }
    }
}
