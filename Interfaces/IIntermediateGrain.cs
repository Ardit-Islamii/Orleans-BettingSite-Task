using Orleans;
using Orleans_BettingSite_Task.Responses;

namespace Orleans_BettingSite_Task.Interfaces
{
    public interface IIntermediateGrain : IGrainWithGuidKey
    {
        Task<BetReadResponse> GetBet();
        Task<BetCreateResponse> SetBetAmount();

    }
}
