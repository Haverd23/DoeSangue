using DOS.Core.Mediator.Commands;
using DOS.Usuario.Application.Commands;
using DOS.Usuario.Domain;

namespace DOS.Usuario.Application.CommandsHandlers
{
    public class AlterarTelefoneCommandHandler : ICommandHandler
        <AlterarTelefoneCommand, bool>
    {
        private readonly IUsuarioRepository _repository;

        public AlterarTelefoneCommandHandler(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> HandleAsync(AlterarTelefoneCommand command)
        {

            var usuario = await _repository.GetById(command.UserId);
            if (usuario == null) throw new ApplicationException("Usuário não encontrado");
            usuario.AlterTelefone(command.Telefone);
            var sucesso = await _repository.UnitOfWork.Commit();
            if (!sucesso) throw new ApplicationException("Não foi possível alterar o telefone");
            return true;
        }
    }
}
