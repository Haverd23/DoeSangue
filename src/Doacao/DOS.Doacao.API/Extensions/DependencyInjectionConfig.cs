using DOS.Core.Mediator.Commands;
using DOS.Doacao.Application.Commands;
using DOS.Doacao.Application.CommandsHandlers;
using DOS.Doacao.Data;
using DOS.Doacao.Data.Mediator;
using DOS.Doacao.Domain;
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
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DoacaoContext>(options =>
                        options.UseSqlServer(connectionString));


            return services;

        }

    }
}
