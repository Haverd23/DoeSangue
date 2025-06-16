using DOS.Core.DomainObjects;
using DOS.Core.Mediator.Commands;
using DOS.Core.Message;
using DOS.Doacao.Application.Commands;
using DOS.Doacao.Application.CommandsHandlers;
using DOS.Doacao.Data;
using DOS.Doacao.Data.EventDispatching;
using DOS.Doacao.Data.Mediator;
using DOS.Doacao.Data.Message;
using DOS.Doacao.Domain;
using DOS.Usuario.Data;
using DOS.Usuario.Domain;
using Microsoft.EntityFrameworkCore;

namespace DOS.Doacao.API.Extensions
{
    public static class DependencyInjectionConfig 
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IDoacaoRepository, DoacaoRepository>();
            services.AddScoped<ICommandHandler<AgendarDoacaoCommand, Guid>, AgendarDoacaoCommandHandler>();
            services.AddScoped<ICommandHandler<DoacaoRealizadaCommand,bool>, DoacaoRealizadaCommandHandler>();
            services.AddScoped<ICommandHandler<DoacaoFinalizadaCommand, bool>, DoacaoFinalizadaCommandHandler>();
            services.AddScoped<ICommandHandler<DoacaoFalhaCommand, bool>, DoacaoFalhaCommandHandler>();
            services.AddScoped<ICommandHandler<DoacaoCanceladaCommand, bool>, DoacaoCanceladaCommandHandler>();


            services.AddScoped<ICommandDispatcher, CommandDispatcher>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddSingleton<IKafkaProducer>(provider => new KafkaProducer("localhost:9092"));
            services.AddScoped<IDomainEventDispatcher, EventDispatching>();


            var connectionString = configuration["DEFAULT_CONNECTION"];
            services.AddDbContext<DoacaoContext>(options =>
                        options.UseSqlServer(connectionString));

            services.AddDbContext<UsuarioContext>(options =>
                        options.UseSqlServer(connectionString));


            return services;

        }

    }
}
