﻿using DOS.Agenda.Application.Commands;
using DOS.Agenda.Application.CommandsHandlers;
using DOS.Agenda.Application.DTOs;
using DOS.Agenda.Application.Kafka.EventosHandlers;
using DOS.Agenda.Application.Queries;
using DOS.Agenda.Application.QueriesHandlers;
using DOS.Agenda.Data;
using DOS.Agenda.Data.Kafka;
using DOS.Agenda.Data.Mediator;
using DOS.Agenda.Domain;
using DOS.Core.Mediator.Commands;
using DOS.Core.Mediator.Queries;
using Microsoft.EntityFrameworkCore;
namespace DOS.Agenda.API.Extensions
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IHorarioRepository, HorarioRepository>();
            services.AddScoped<ICommandHandler<AgendaCriadaCommand, Guid>, AgendaCriadaCommandHandler>();
            services.AddScoped<ICommandHandler<DeletarAgendaCommand, bool>, DeletarAgendaCommandHandler>();
            services.AddScoped<ICommandHandler<AtualizarDataHoraCommand,bool>, AtualizarDataHoraCommandHandler>();
            services.AddScoped<ICommandHandler<AtualizarQuantidadeVagasCommand, bool>, AtualizarQuantidadeVagasCommandHandler>();
            services.AddScoped<IQueryHandler<ListarHorariosQuery, IEnumerable<AgendaDTO>>, ListarHorariosQueryHandler>();
            services.AddHostedService<KafkaConsumerService>();
            services.AddScoped<DoacaoCanceladaEventHandler>();
            services.AddScoped<DoacaoAgendadaEventHandler>();

            services.AddScoped<ICommandDispatcher, CommandDispatcher>();
            services.AddScoped<IQueryDispatcher, QueryDispatcher>();


            var connectionString = configuration["DEFAULT_CONNECTION"];
            services.AddDbContext<HorarioContext>(options =>
                options.UseSqlServer(connectionString));



            return services;
        }

    }
}