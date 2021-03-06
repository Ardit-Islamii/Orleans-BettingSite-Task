using Orleans;

namespace Orleans_BettingSite_Task.Interfaces
{
    public interface IBetGrain : IGrainWithGuidKey
    {
        Task<decimal> GetBetAmount();
        Task<decimal> SetBetAmountAsync(decimal amount);
    }
}
