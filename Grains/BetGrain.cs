using Orleans;
using Orleans.Providers;
using Orleans.Runtime;
using Orleans.Streams;
using Orleans_BettingSite_Task.Interfaces;
using Orleans_BettingSite_Task.States;
using Orleans_BettingSite_Task.Streams;

namespace Orleans_BettingSite_Task.Grains
{
    [StorageProvider(ProviderName = "amountStore")]
    public class BetGrain : Grain, IBetGrain
    {
        private IAsyncObservable<BetMessage> consumer;
        internal int numConsumedItems;
        internal ILogger logger;
        private StreamSubscriptionHandle<BetMessage> consumerHandle;

        private readonly IPersistentState<AmountState> _amount;

        public BetGrain([PersistentState("amount", "amountStore")] IPersistentState<AmountState> amount)
        {
            _amount = amount;
        }

        public override async Task OnActivateAsync()
        {
            IStreamProvider streamProvider = this.GetStreamProvider("bet");
            consumer = streamProvider.GetStream<BetMessage>(this.GetPrimaryKey(), "default");
            consumerHandle = await consumer.SubscribeAsync(new StreamObserver());
            await base.OnActivateAsync();
        }

        public async Task<decimal> GetBetAmountAsync()
        {
            return _amount.State.Amount;
        }

        public async Task<decimal> SetBetAmountAsync(decimal amount)
        {
            _amount.State.Amount = amount;
            await _amount.WriteStateAsync();
            return await Task.FromResult(_amount.State.Amount);
        }
    }
}
