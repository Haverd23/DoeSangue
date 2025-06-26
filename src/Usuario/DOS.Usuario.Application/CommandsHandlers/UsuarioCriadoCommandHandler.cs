using DOS.Core.DomainObjects;
using DOS.Core.Enums;
using DOS.Core.Mediator.Commands;
using DOS.Usuario.Application.Commands;
using DOS.Usuario.Domain;

namespace DOS.Usuario.Application.CommandsHandlers
{
    public class UsuarioCriadoCommandHandler : ICommandHandler<UsuarioCriadoCommand, Guid>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IDomainEventDispatcher _domainEventDispatcher;


        public UsuarioCriadoCommandHandler(IUsuarioRepository usuarioRepository,
            IDomainEventDispatcher domainEventDispatcher)
        {
            _usuarioRepository = usuarioRepository;
            _domainEventDispatcher = domainEventDispatcher;
        }
        public async Task<Guid> HandleAsync(UsuarioCriadoCommand command)
        {
            var tipoSanguineo = (TipoSanguineo)Enum.Parse(typeof(TipoSanguineo), command.TipoSanguineo, ignoreCase: true);
            var usuario = new User(command.Id,command.Nome,command.Email, command.CPF, command.Telefone, tipoSanguineo);
            await _usuarioRepository.Adcionar(usuario);
            var sucesso = await _usuarioRepository.UnitOfWork.Commit();
            if (!sucesso)
            {
                throw new ApplicationException("Erro ao salvar o usuário");
            }
            await _domainEventDispatcher.DispatchEventsAsync(usuario.DomainEvents);
            return usuario.Id;
        }
    }
}

