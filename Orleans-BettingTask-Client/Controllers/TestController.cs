﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using Orleans_BettingSite_Task.Interfaces;
using Orleans_BettingSite_Task.Responses;

namespace Orleans_BettingTask_Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IClusterClient _client;

        public TestController(IClusterClient client)
        {
            _client = client;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BetReadResponse>> GetBetGrainAsync(Guid id)
        {
            var betGrain = _client.GetGrain<IIntermediateGrain>(id);
            var test = await betGrain.GetBetAsync(id);
            return Ok(test);
        }
    }
}
