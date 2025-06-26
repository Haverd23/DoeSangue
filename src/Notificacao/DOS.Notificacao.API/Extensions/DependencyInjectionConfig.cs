using DOS.Notificacao.Application.EventsHandlers.Doacao;
using DOS.Notificacao.Application.EventsHandlers.Usuario;
using DOS.Notificacao.Application.Kafka;
using DOS.Notificacao.Data;
using DOS.Notificacao.Domain;

namespace DOS.Notificacao.API.Extensions
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services,
            IConfiguration configuration)
        {
            // Eventos
            services.AddScoped<UsuarioCriadoEventHandler>();
            services.AddScoped<DoacaoAgendadaEventHandler>();
            services.AddScoped<DoacaoRealizadaEventHandler>();
            services.AddScoped<DoacaoFinalizadaEventHandler>();
            services.AddScoped<DoacaoFalhaEventHandler>();


            // Email
            var emailSection = configuration.GetSection("Email");
            var smtpHost = emailSection.GetValue<string>("smtpHost");
            var smtpPort = emailSection.GetValue<int>("smtpPort");
            var smtpUser = emailSection.GetValue<string>("smtpUser");
            var smtpPass = emailSection.GetValue<string>("smtpPass");
            var from = emailSection.GetValue<string>("from");

            services.AddSingleton<IEmailService>(provider =>
                new EmailService(smtpHost, smtpPort, smtpUser, smtpPass, smtpUseSsl: true));

            // Kafka
            var kafkaBootstrapServers = configuration["Kafka:BootstrapServers"];
            services.AddSingleton<KafkaEventRegistry>();
            services.AddHostedService<KafkaConsumerService>(provider =>
            {
                var registry = provider.GetRequiredService<KafkaEventRegistry>();
                var bootstrapServers = kafkaBootstrapServers;
                var groupId = "notificacao-consumer-group";
                return new KafkaConsumerService(provider, registry, bootstrapServers, groupId);
            });

            return services;

        }
    }
}
