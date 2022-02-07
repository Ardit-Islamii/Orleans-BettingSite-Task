using Orleans;
using Orleans_BettingSite_Task.Interfaces;

namespace Orleans_BettingSite_Task.Grains
{
    public class BetGrain : Grain, IBetGrain
    {
        public Task<decimal> GetBetAmount()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetBetAmount(decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}
