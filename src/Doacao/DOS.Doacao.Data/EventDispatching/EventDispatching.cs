using DOS.Core.DomainObjects;
using DOS.Core.Message;
using System.Text.Json;

namespace DOS.Doacao.Data.EventDispatching
{
    public class EventDispatching : IDomainEventDispatcher
    {
        private readonly IKafkaProducer _kafkaProducer;
        public EventDispatching(IKafkaProducer kafkaProducer)
        {
            _kafkaProducer = kafkaProducer;
        }
        public async Task DispatchEventsAsync(IEnumerable<IDomainEvent> events)
        {
            foreach (var domainEvents in events)
            {
                var eventType = domainEvents.GetType().Name;
                var topic = eventType;
                var json = JsonSerializer.Serialize(
                    domainEvents,
                    domainEvents.GetType(),
                    new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        WriteIndented = false
                    }
                    );
                await _kafkaProducer.PublishAsync(topic, Guid.NewGuid().ToString(), json);
            }
        }
    }
}
