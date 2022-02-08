using Orleans;
using Orleans.Runtime;
using Orleans.Streams;
using Orleans_BettingSite_Task.Interfaces;
using Orleans_BettingSite_Task.Streams;

namespace Orleans_BettingSite_Task.Grains
{
    public class BetGrain : Grain, IBetGrain // Qekjo the receiver se ban subscribe -> consumer.SubscribeAsync
    {
        private IAsyncObservable<BetMessage> consumer;
        internal int numConsumedItems;
        internal ILogger logger;
        private StreamSubscriptionHandle<BetMessage> consumerHandle;

        public override Task OnActivateAsync()
        {
            return base.OnActivateAsync();
        }

        public async Task BecomeConsumer(Guid streamId)
        {
            IStreamProvider streamProvider = this.GetStreamProvider("bet");
            consumer = streamProvider.GetStream<BetMessage>(streamId, "default");
            consumerHandle = await consumer.SubscribeAsync(new StreamObserver());
        }

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
