using Orleans;
using Orleans_BettingSite_Task.Interfaces;
using Orleans.Runtime;
using Orleans_BettingSite_Task.ObserverManager;

namespace Orleans_BettingSite_Task.Grains
{
    public class TestGrain : Grain, ITestGrain
    {
        private readonly ObserverManager<ITest> _subsManager;
        public TestGrain(ILogger<TestGrain> logger)
        {
            _subsManager = new ObserverManager<ITest>(TimeSpan.FromMinutes(5), logger, "subs");
        }

        public Task Subscribe(ITest observer)
        {
            _subsManager.Subscribe(observer, observer);
            return Task.CompletedTask;
        }

        public Task UnSubscribe(ITest observer)
        {
            _subsManager.Unsubscribe(observer);
            return Task.CompletedTask;
        }

        public Task SendMessage(string message)
        {
            _subsManager.Notify(x => x.ReceiveMessage(message));
            return Task.CompletedTask;
        }
    }
}
