using DOS.Core.Mediator.Commands;
using DOS.Estoque.Application.Commands;
using DOS.Estoque.Application.CommandsHandlers;
using DOS.Estoque.Data;
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
            services.AddScoped<ICommandHandler<RegistrarDoacaoEstoqueCommand, bool>,
                RegistrarDoacaoEstoqueCommandHandler>();
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();


            var connectionString = configuration["DEFAULT_CONNECTION"];
            services.AddDbContext<EstoqueContext>(options =>
                        options.UseSqlServer(connectionString));

            return services;
        }
    }
}
