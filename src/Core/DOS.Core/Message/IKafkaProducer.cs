namespace DOS.Core.Message
{
    public interface IKafkaProducer
    {
        Task PublishAsync(string topic, string key, string value);
    }
}