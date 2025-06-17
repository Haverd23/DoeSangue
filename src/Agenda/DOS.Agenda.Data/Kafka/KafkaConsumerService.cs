using Confluent.Kafka;
using DOS.Agenda.Application.Kafka.Eventos;
using DOS.Agenda.Application.Kafka.EventosHandlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

namespace DOS.Agenda.Data.Kafka
{
    public class KafkaConsumerService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConsumer<string, string> _consumer;

        private readonly string[] _topics = new[]
        {
            "DoacaoCanceladaEvent"
            
        };

        public KafkaConsumerService(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;

            var bootstrapServers = configuration.GetSection("Kafka:BootstrapServers").Value;

            var config = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = "agenda-consumer-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<string, string>(config).Build();
            _consumer.Subscribe(_topics);
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

                    Console.WriteLine($"Mensagem recebida do tópico {topic}: {message}");

                    using var scope = _serviceProvider.CreateScope();

                    var json = JsonDocument.Parse(message);

                    var agendaId = json.RootElement.GetProperty("agendaId").GetGuid();

                    switch (topic)
                    {
                        case "DoacaoCanceladaEvent":
                            var canceladaEvent = new DoacaoCanceladaEvent
                            {
                                AgendaId = agendaId
                            };
                            var canceladaHandler = scope.ServiceProvider.GetRequiredService<DoacaoCanceladaEventHandler>();
                            await canceladaHandler.HandleAsync(canceladaEvent);
                            break;

                     
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao processar evento do Kafka: {ex.Message}");
                }
            }
        }
    }
}