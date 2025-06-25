using DOS.Core.Mediator.Commands;
using DOS.Core.Mediator.Queries;
using DOS.Estoque.Application.Commands;
using DOS.Estoque.Application.CommandsHandlers;
using DOS.Estoque.Application.DTOs;
using DOS.Estoque.Application.Kafka.EventosHandlers;
using DOS.Estoque.Application.Queries;
using DOS.Estoque.Application.QueriesHandlers;
using DOS.Estoque.Data;
using DOS.Estoque.Data.Kafka;
using DOS.Estoque.Data.Mediator;
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

            services.AddScoped<IQueryHandler<ListarEstoqueQuery, IEnumerable<EstoqueDTO>>, ListarEstoqueQueryHandler>();
            services.AddScoped<IQueryDispatcher, QueryDispatcher>();
            services.AddScoped<ICommandHandler<RetirarUnidadeSanguineaCommand, bool>, RetirarUnidadeSanguineaCommandHandler>();
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();

            services.AddHostedService<KafkaConsumerService>();
            services.AddScoped<DoacaoFinalizadaEventHandler>();




            var connectionString = configuration["DEFAULT_CONNECTION"];
            services.AddDbContext<EstoqueContext>(options =>
                        options.UseSqlServer(connectionString));

            return services;
        }
    }
}
