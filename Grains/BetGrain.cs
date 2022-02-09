﻿using Orleans;
using Orleans.Providers;
using Orleans.Runtime;
using Orleans.Streams;
using Orleans_BettingSite_Task.Interfaces;
using Orleans_BettingSite_Task.States;
using Orleans_BettingSite_Task.Streams;

namespace Orleans_BettingSite_Task.Grains
{
    [StorageProvider(ProviderName = "amountStore")]
    [ImplicitStreamSubscription("default")]
    public class BetGrain : Grain, IBetGrain
    {
        private IAsyncObservable<BetMessage> consumer;
        internal ILogger logger;
        private StreamSubscriptionHandle<BetMessage> consumerHandle;

        private readonly IPersistentState<AmountState> _amount;

        public BetGrain([PersistentState("amount", "amountStore")] IPersistentState<AmountState> amount, ILoggerFactory loggerFactory)
        {
            _amount = amount;
            logger = loggerFactory.CreateLogger($"{this.GetType().Name}-{this.IdentityString}");
        }
        public override async Task OnActivateAsync()
        {
            var streamProvider = GetStreamProvider("bet");
            var stream = streamProvider.GetStream<BetMessage>(this.GetPrimaryKey(), "default");
            await stream.SubscribeAsync(OnNextAsync, OnErrorAsync, OnCompletedAsync);
        }
        public async Task OnNextAsync(BetMessage item, StreamSequenceToken token = null)
        {
            logger.Info("OnNextAsync({0}{1})", item, token != null ? token.ToString() : "null");
            await SetBetAmountAsync(item.Amount);
            await Task.CompletedTask;
        }
        public Task OnCompletedAsync()
        {
            logger.Info("OnCompletedAsync()");
            return Task.CompletedTask;
        }
        public Task OnErrorAsync(Exception ex)
        {
            logger.Info("OnErrorAsync({0})", ex);
            return Task.CompletedTask;
        }
        public async Task<decimal> GetBetAmountAsync()
        {
            var amount = _amount.State.Amount;
            return amount;
        }
        public async Task<decimal> SetBetAmountAsync(decimal amount)
        {
            _amount.State.Amount = amount;
            await _amount.WriteStateAsync();
            return await Task.FromResult(_amount.State.Amount);
        }
    }
}
