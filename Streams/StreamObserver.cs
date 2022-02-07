using Orleans.Streams;

namespace Orleans_BettingSite_Task.Streams
{
    public class StreamObserver : IAsyncStream<T>
    {
        public StreamObserver()
        {
            
        }
        public Task OnCompletedAsync() => Task.CompletedTask;

        public Task OnErrorAsync(Exception ex)
            
            return Task.CompletedTask;
        }
        public Task OnNextAsync(T item, StreamSequenceToken token = null)
        {
            return Task.CompletedTask;
        }
    }
}
