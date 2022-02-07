using Orleans;
using Orleans_BettingSite_Task.Interfaces;
using Orleans_BettingSite_Task.Responses;

namespace Orleans_BettingSite_Task.Grains
{
    public class IntermediateGrain : Grain, IIntermediateGrain
    {
        public Task<BetReadResponse> GetBet()
        {
            throw new NotImplementedException();
        }

        public Task<BetCreateResponse> SetBetAmount()
        {
            throw new NotImplementedException();
        }
    }
}
