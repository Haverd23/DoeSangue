using Confluent.Kafka;
using DOS.Notificacao.Application.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DOS.Notificacao.Data
{
    public class KafkaConsumerService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly KafkaEventRegistry _registry;
        private readonly IConsumer<string, string> _consumer;

        public KafkaConsumerService(
            IServiceProvider serviceProvider,
            KafkaEventRegistry registry,
            string bootstrapServers,
            string groupId)
        {
            _serviceProvider = serviceProvider;
            _registry = registry;

            var config = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = groupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<string, string>(config).Build();
            _consumer.Subscribe(_registry.GetTopics());
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var result = _consumer.Consume(stoppingToken);
                    var topic = result.Topic;
                    var message = result.Message.Value;

                    if (_registry.TryGetTypes(topic, out var eventType, out var handlerType))
                    {
                        using var scope = _serviceProvider.CreateScope();

                        var handler = scope.ServiceProvider.GetService(handlerType);

                        if (handler == null)
                        {
                            Console.WriteLine($"Handler não encontrado para o tópico {topic}");
                            continue;
                        }

                        var evento = _registry.DeserializeEvent(message, eventType);

                        var handleMethod = handlerType.GetMethod("HandleAsync");
                        if (handleMethod != null)
                        {
                            var task = (Task)handleMethod.Invoke(handler, new[] { evento })!;
                            await task;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Nenhum mapeamento encontrado para o tópico {topic}");
                    }
                }
                catch (ConsumeException ex)
                {
                    Console.WriteLine($"Erro ao consumir mensagem: {ex.Error.Reason}");
                }


            }
        }
        
        

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _consumer.Close();
            await base.StopAsync(cancellationToken);
        }
    }
}
