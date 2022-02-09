using Orleans;
using Orleans.Streams;
using Orleans_BettingSite_Task.Interfaces;
using Orleans_BettingSite_Task.Responses;

namespace Orleans_BettingSite_Task.Grains
{
    public class IntermediateGrain : Grain, IIntermediateGrain
    {
        private IBetGrain currentBet;
        private IAsyncStream<BetMessage> stream;

        public IntermediateGrain()
        {
            
        }

        public override Task OnActivateAsync()
        {
            var streamProvider = GetStreamProvider("bet");
            stream = streamProvider.GetStream<BetMessage>(this.GetPrimaryKey(), "default");
            currentBet = GrainFactory.GetGrain<IBetGrain>(this.GetPrimaryKey());
            return base.OnActivateAsync();
        }

        public async Task<BetReadResponse> GetBetAsync(Guid betId)
        {
            var result = await currentBet.GetBetAmountAsync();
            var betReadResponse = new BetReadResponse()
            {
                Amount = result,
                Id = this.GetPrimaryKey(),
                LastUpdated = DateTime.UtcNow
            };
            return await Task.FromResult(betReadResponse);
        }

        public async Task<BetCreateResponse> SetBetAmountAsync(decimal amount)
        {
            var result = await currentBet.SetBetAmountAsync(amount);
            var returnedResult = new BetCreateResponse()
            {
                Amount = result,
                Id = this.GetPrimaryKey(),
                LastUpdated = DateTime.Now
            };
            return await Task.FromResult(returnedResult);
        }
    }
}
