using DOS.Auth.Application.Commands;
using DOS.Auth.Application.Services;
using DOS.Auth.Application.Services.Interfaces;
using DOS.Auth.Domain.Interfaces;
using DOS.Auth.Domain.Models;
using DOS.Core.DomainObjects;
using DOS.Core.Mediator.Commands;

namespace DOS.Auth.Application.CommandsHandlers
{
    public class UsuarioCriadoCommandHandler : ICommandHandler<UsuarioCriadoCommand, Guid>
    {
        private readonly IUserRepository _usuarioRepository;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly ISenhaCriptografia _senhaCriptografia;
        public UsuarioCriadoCommandHandler(IUserRepository usuarioRepository, IDomainEventDispatcher
            domainEventDispatcher, ISenhaCriptografia passwordHasher)
        {
            _usuarioRepository = usuarioRepository;
            _domainEventDispatcher = domainEventDispatcher;
            _senhaCriptografia = passwordHasher;
        }

        public async Task<Guid> HandleAsync(UsuarioCriadoCommand command)
        {
            User.SenhaValida(command.Senha);

            var senhaHash = _senhaCriptografia.SenhaHash(command.Senha);

            var user = new User(command.Email, senhaHash);

            await _usuarioRepository.AdicionarUser(user);

            var sucesso = await _usuarioRepository.UnitOfWork.Commit();
            if (!sucesso)
            {
                throw new ApplicationException("Erro ao salvar o usuário");
            }
            await _domainEventDispatcher.DispatchEventsAsync(user.GetDomainEvents());

            return user.Id;

        }
    }
}
