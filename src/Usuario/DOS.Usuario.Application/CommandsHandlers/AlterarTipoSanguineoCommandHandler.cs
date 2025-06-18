using DOS.Core.Mediator.Commands;
using DOS.Usuario.Application.Commands;
using DOS.Usuario.Domain;
using DOS.Usuario.Domain.Enums;
using DOS.Usuario.Domain.ValueObjects;
namespace DOS.Usuario.Application.CommandsHandlers
{
    public class AlterarTipoSanguineoCommandHandler : ICommandHandler
        <AlterarTipoSanguineoCommand,bool>
    {
        private readonly IUsuarioRepository _repository;

        public AlterarTipoSanguineoCommandHandler(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> HandleAsync(AlterarTipoSanguineoCommand command)
        {
            var cpf = new CPF(command.CPF);
            var usuario = await _repository.GetByCPF(cpf);
            if (usuario == null)
                throw new Exception("Usuário não encontrado");

            bool conversaoOk = Enum.TryParse<TipoSanguineo>(
               command.TipoSanguineo,
               ignoreCase: true,
               out var tipoSanguineoConvertido);

            if (!conversaoOk)
                throw new Exception("Tipo sanguíneo inválido.");

            usuario.AlterarTipoSanguineo(tipoSanguineoConvertido);

            var sucesso = await _repository.UnitOfWork.Commit();

            if (!sucesso)
                throw new Exception("Erro ao atualizar o tipo sanguíneo.");

            return true;
        }
    }
}
