using Confluent.Kafka;
using DOS.Estoque.Application.Kafka.Eventos;
using DOS.Estoque.Application.Kafka.EventosHandlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
namespace DOS.Estoque.Data.Kafka
{
    public class KafkaConsumerService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        private readonly string[] _topics = new[]
        {
           
            "DoacaoFinalizadaEvent"
        };

        public KafkaConsumerService(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var bootstrapServers = _configuration.GetSection("Kafka:BootstrapServers").Value;

            var config = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = "agenda-consumer-group",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = true,
                SocketTimeoutMs = 10000,
                SessionTimeoutMs = 10000
            };

            using var consumer = new ConsumerBuilder<string, string>(config).Build();
            consumer.Subscribe(_topics);

            Console.WriteLine("🚀 Kafka Consumer iniciado e ouvindo tópicos...");

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var result = consumer.Consume(TimeSpan.FromSeconds(1));

                        if (result == null)
                        {
                            await Task.Delay(500, stoppingToken);
                            continue;
                        }

                        Console.WriteLine($"✅ Mensagem recebida do tópico {result.Topic}: {result.Message.Value}");

                        using var scope = _serviceProvider.CreateScope();
                        await ProcessMessageAsync(result.Topic, result.Message.Value, scope);
                    }
                    catch (ConsumeException ex)
                    {
                        Console.WriteLine($"⚠️ Erro ao consumir mensagem do Kafka: {ex.Error.Reason}");
                        await Task.Delay(2000, stoppingToken);
                    }
                    catch (OperationCanceledException)
                    {
                        Console.WriteLine("⛔ Cancelamento solicitado.");
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ Erro inesperado: {ex.Message}");
                        await Task.Delay(2000, stoppingToken);
                    }
                }
            }
            finally
            {
                consumer.Close();
                Console.WriteLine("🛑 Kafka Consumer encerrado.");
            }
        }

        private async Task ProcessMessageAsync(string topic, string message, IServiceScope scope)
        {
            try
            {
                var json = JsonDocument.Parse(message);

                switch (topic)
                {
                    case "DoacaoFinalizadaEvent": 
                        var finalizadaEvent = JsonSerializer.Deserialize<DoacaoFinalizadaEvent>(message);
                        var finalizadaHandler = scope.ServiceProvider.GetRequiredService<DoacaoFinalizadaEventHandler>();
                        await finalizadaHandler.HandleAsync(finalizadaEvent);
                        break;

                   

                    default:
                        Console.WriteLine($"⚠️ Tópico desconhecido: {topic}");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro no processamento da mensagem: {ex.Message}");
            }
        }
    }
}