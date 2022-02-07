namespace Orleans_BettingSite_Task.Responses
{
    public class BetCreateResponse
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
