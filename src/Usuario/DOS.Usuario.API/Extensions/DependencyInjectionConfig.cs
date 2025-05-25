using DOS.Core.Mediator.Commands;
using DOS.Usuario.Application.Commands;
using DOS.Usuario.Application.CommandsHandlers;
using DOS.Usuario.Data;
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


            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<UsuarioContext>(options =>
                        options.UseSqlServer(connectionString));


            return services;
        }

    }
}
