using Orleans;
using Orleans.Streams;
using Orleans_BettingSite_Task.Interfaces;
using Orleans_BettingSite_Task.Responses;

namespace Orleans_BettingSite_Task.Grains
{
    public class IntermediateGrain : Grain, IIntermediateGrain //Qeta llogaritne si ni GrainProducer a qka hamami ken
    {
        private IAsyncStream<BetMessage> stream;


        public override Task OnActivateAsync()
        {
            var streamProvider = GetStreamProvider("bet");
            stream = streamProvider.GetStream<BetMessage>(this.GetPrimaryKey(), "default");
            return base.OnActivateAsync();
        }

        public async Task<BetReadResponse> GetBet()
        {
            await stream.OnNextAsync(new BetMessage(10, "test"));
            return await Task.FromResult(new BetReadResponse()
            {
                Id = Guid.NewGuid(),
                Amount = 10,
                LastUpdated = DateTime.UtcNow
            });
        }

        public Task<BetCreateResponse> SetBetAmount()
        {
            throw new NotImplementedException();
        }
    }
}
