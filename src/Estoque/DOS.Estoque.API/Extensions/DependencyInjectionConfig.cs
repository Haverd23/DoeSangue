using DOS.Estoque.Application.Kafka.EventosHandlers;
using DOS.Estoque.Data;
using DOS.Estoque.Data.Kafka;
using DOS.Estoque.Domain;
using Microsoft.EntityFrameworkCore;

namespace DOS.Estoque.API.Extensions
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IEstoqueRepository, EstoqueRepository>();


            services.AddHostedService<KafkaConsumerService>();
            services.AddScoped<DoacaoFinalizadaEventHandler>();




            var connectionString = configuration["DEFAULT_CONNECTION"];
            services.AddDbContext<EstoqueContext>(options =>
                        options.UseSqlServer(connectionString));

            return services;
        }
    }
}
