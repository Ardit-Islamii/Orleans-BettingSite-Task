using Orleans;

namespace Orleans_BettingSite_Task.Interfaces
{
    public interface IBetGrain : IGrainWithGuidKey
    {
        Task<decimal> GetBetAmount();
        Task<bool> SetBetAmount(decimal amount);

    }
}
