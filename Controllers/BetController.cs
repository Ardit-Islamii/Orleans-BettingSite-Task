using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using Orleans_BettingSite_Task.Grains;
using Orleans_BettingSite_Task.Interfaces;
using Orleans_BettingSite_Task.ObserverClasses;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<BetReadResponse>> GetBet(Guid id)
        {
            var intermediateGrain = _factory.GetGrain<IIntermediateGrain>(id);
            var result = await intermediateGrain.GetBetAsync(id);
            return Ok(result);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<BetCreateResponse>> CreateBet(Guid id,[FromBody] BetCreateRequest betRequest)
        {
            var intermediateGrain = _factory.GetGrain<IIntermediateGrain>(id);
            var result = await intermediateGrain.SetBetAmountAsync(betRequest.Amount);
            return Ok(result);
        }


        [HttpPost("{id}")]
        public async Task SubscribeToTestGrain(Guid id)
        {
            var testGrain = _factory.GetGrain<ITestGrain>(id);
            Test test = new Test();
            var obj = await _factory.CreateObjectReference<ITest>(test);

            await testGrain.Subscribe(obj);
        }
    }
}
