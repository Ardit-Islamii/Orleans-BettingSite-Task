using Orleans;
using Orleans_BettingSite_Task.Responses;

namespace Orleans_BettingSite_Task.Interfaces
{
    public interface IIntermediateGrain : IGrainWithGuidKey
    {
        Task<BetReadResponse> GetBetAsync(Guid betId);
        Task<BetCreateResponse> SetBetAmountAsync(decimal amount);
    }
}
