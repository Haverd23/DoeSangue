using DOS.Auth.Application.Commands;
using DOS.Auth.Application.Eventos;
using DOS.Auth.Application.Services.Interfaces;
using DOS.Auth.Domain.Interfaces;
using DOS.Auth.Domain.Models;
using DOS.Core.DomainObjects;
using DOS.Core.Mediator.Commands;

namespace DOS.Auth.Application.CommandsHandlers
{
    public class EsqueciASenhaCommandHandler : ICommandHandler<EsqueciASenhaCommand,bool>
    {
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly IUserRepository _userRepository;
        private readonly ITokenJWT _token;

        public EsqueciASenhaCommandHandler(IDomainEventDispatcher domainEventDispatcher,
            IUserRepository userRepository, ITokenJWT token)
        {
            _domainEventDispatcher = domainEventDispatcher;
            _userRepository = userRepository;
            _token = token;
        }

        public async Task<bool> HandleAsync(EsqueciASenhaCommand command)
        {
            var email = new Email(command.Email);
            var usuario = await _userRepository.ObterPorEmail(email);
            if (usuario == null) throw new Exception("Usuário não encontrado");

            var token = await _token.GerarTokenRecuperacaoSenhaAsync(usuario);

            await _domainEventDispatcher.DispatchEventsAsync(
                new List<IDomainEvent>
                {
                    new RecuperacaoDeSenhaSolicitadaEvent(
                        usuario.Id,
                        usuario.Email.Entrada,
                        token
                        )
                });
            return true;
        }
    }
}
