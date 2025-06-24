using DOS.Auth.Application.Commands;
using DOS.Auth.Application.CommandsHandlers;
using DOS.Auth.Application.Services;
using DOS.Auth.Application.Services.Interfaces;
using DOS.Auth.Data;
using DOS.Auth.Data.EventDispatching;
using DOS.Auth.Data.Mediator;
using DOS.Auth.Domain.Interfaces;
using DOS.Core.DomainObjects;
using DOS.Core.Mediator.Commands;
using Microsoft.EntityFrameworkCore;

namespace DOS.Auth.API.Extensions
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ISenhaCriptografia, SenhaCriptografia>();
            services.AddScoped<ITokenJWT, TokenJWT>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDomainEventDispatcher, UserCriadoEventDispatching>();
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();
            services.AddScoped<ICommandHandler<UsuarioCriadoCommand, Guid>, UsuarioCriadoCommandHandler>();
            services.AddScoped<ICommandHandler<AlterarSenhaCommand,bool>, AlterarSenhaCommandHandler>();
            services.AddScoped<ICommandHandler<AlterarEmailCommand,bool>, AlterarEmailCommandHandler>();

            var connectionString = configuration["DEFAULT_CONNECTION"];
            services.AddDbContext<UserContext>(options =>
                        options.UseSqlServer(connectionString));


            return services;
        }
    }
}
