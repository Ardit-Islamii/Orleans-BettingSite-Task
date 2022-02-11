using Orleans_BettingSite_Task.Interfaces;

namespace Orleans_BettingSite_Task.ObserverClasses
{
    public class Test : ITest
    {
        public void ReceiveMessage(string message)
        {
            Console.WriteLine($"Successfully received message: {message}");
        }
    }
}
