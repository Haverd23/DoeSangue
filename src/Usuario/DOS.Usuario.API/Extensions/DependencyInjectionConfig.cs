using DOS.Core.DomainObjects;
using DOS.Core.Mediator.Commands;
using DOS.Core.Mediator.Queries;
using DOS.Core.Message;
using DOS.Doacao.Data;
using DOS.Doacao.Domain;
using DOS.Usuario.Application.Commands;
using DOS.Usuario.Application.CommandsHandlers;
using DOS.Usuario.Application.DTOs;
using DOS.Usuario.Application.Queries;
using DOS.Usuario.Application.QueriesHandlers;
using DOS.Usuario.Application.Services;
using DOS.Usuario.Data;
using DOS.Usuario.Data.EventDispatching;
using DOS.Usuario.Data.Mediator;
using DOS.Usuario.Data.Message;
using DOS.Usuario.Domain;
using Microsoft.EntityFrameworkCore;

namespace DOS.Usuario.API.Extensions
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IDoacaoRepository, DoacaoRepository>();

            services.AddScoped<ICommandHandler<UsuarioCriadoCommand, Guid>, UsuarioCriadoCommandHandler>();
            services.AddScoped<ICommandHandler<AlterarTelefoneCommand,bool>, AlterarTelefoneCommandHandler>();
            services.AddScoped<ICommandHandler<AlterarTipoSanguineoCommand,bool>, AlterarTipoSanguineoCommandHandler>();
            services.AddScoped<IQueryHandler<DoacaoHistoricoQuery, IEnumerable<HistoricoDoacaoDTO>>, DoacaoHistoricoQueryHandler>();

            services.AddScoped<IDomainEventDispatcher, EventDispatching>();

            var kafkaBootstrapServers = configuration["Kafka:BootstrapServers"];
            services.AddSingleton<IKafkaProducer>(provider => new KafkaProducer(kafkaBootstrapServers));


            services.AddScoped<IQueryDispatcher, QueryDispatcher>();
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();
            services.AddScoped<IDoacaoService, DoacaoService>();

            var connectionString = configuration["DEFAULT_CONNECTION"];
            services.AddDbContext<UsuarioContext>(options =>
                        options.UseSqlServer(connectionString));

            services.AddDbContext<DoacaoContext>(options =>
                        options.UseSqlServer(connectionString));

            return services;
        }

    }
}
