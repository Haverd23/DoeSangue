namespace DOS.Core.Message
{
    public interface IKafkaConsumer
    {
        Task SubscribeAsync(string topic, Func<string, string, Task> messageHandler, CancellationToken cancellationToken = default);

    }
}