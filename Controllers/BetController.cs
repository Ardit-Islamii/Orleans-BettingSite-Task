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
        public async Task<ActionResult<BetCreateResponse>> CreateBet([FromBody] BetCreateRequest betRequest)
        {
            var storedGuid = Guid.NewGuid();
            var intermediateGrain = _factory.GetGrain<IIntermediateGrain>(storedGuid);
            var betGrain = _factory.GetGrain<IBetGrain>(storedGuid);
            await betGrain.BecomeConsumer(storedGuid);
            var test = await intermediateGrain.GetBet();
            var testObj = new BetCreateResponse()
            {
                Id = Guid.NewGuid(),
                Amount = 10,
                LastUpdated = DateTime.UtcNow
            };
            
            return Ok(testObj);
        }
    }
}
