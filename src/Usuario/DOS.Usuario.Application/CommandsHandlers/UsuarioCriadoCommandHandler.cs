using DOS.Core.Mediator.Commands;
using DOS.Usuario.Application.Commands;
using DOS.Usuario.Domain;

namespace DOS.Usuario.Application.CommandsHandlers
{
    public class UsuarioCriadoCommandHandler : ICommandHandler<UsuarioCriadoCommand, Guid>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioCriadoCommandHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public async Task<Guid> HandleAsync(UsuarioCriadoCommand command)
        {
            var usuario = new User(command.Nome,command.Email, command.CPF, command.Telefone, command.TipoSanguineo);
            await _usuarioRepository.Adcionar(usuario);
            var sucesso = await _usuarioRepository.UnitOfWork.Commit();
            if (!sucesso)
            {
                throw new Exception("Erro ao salvar o usuário");
            }
            return usuario.Id;
        }
    }
}

