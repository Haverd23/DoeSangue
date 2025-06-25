using DOS.Core.DomainObjects;
using DOS.Core.Mediator.Commands;
using DOS.Core.Mediator.Queries;
using DOS.Core.Message;
using DOS.Usuario.Application.Commands;
using DOS.Usuario.Application.CommandsHandlers;
using DOS.Usuario.Application.DTOs;
using DOS.Usuario.Application.Queries;
using DOS.Usuario.Application.QueriesHandlers;
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
            services.AddScoped<ICommandHandler<UsuarioCriadoCommand, Guid>, UsuarioCriadoCommandHandler>();
            services.AddScoped<ICommandHandler<AlterarTelefoneCommand,bool>, AlterarTelefoneCommandHandler>();
            services.AddScoped<ICommandHandler<AlterarTipoSanguineoCommand,bool>, AlterarTipoSanguineoCommandHandler>();
            services.AddScoped < IQueryHandler<DoacaoHistoricoQuery, IEnumerable<HistoricoDoacaoDTO>>, DoacaoHistoricoQueryHandler>();

            services.AddScoped<IDomainEventDispatcher, EventDispatching>();
            services.AddSingleton<IKafkaProducer>(provider => new KafkaProducer("localhost:9092"));


            services.AddScoped<IQueryDispatcher, QueryDispatcher>();
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();

            var connectionString = configuration["DEFAULT_CONNECTION"];
            services.AddDbContext<UsuarioContext>(options =>
                        options.UseSqlServer(connectionString));


            return services;
        }

    }
}
