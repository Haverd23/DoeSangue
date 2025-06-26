using DOS.Agenda.Application.Commands;
using DOS.Agenda.Domain;
using DOS.Core.Exceptions;
using DOS.Core.Exceptions.DOS.Core.Exceptions;
using DOS.Core.Mediator.Commands;

namespace DOS.Agenda.Application.CommandsHandlers
{
    public class AtualizarQuantidadeVagasCommandHandler : ICommandHandler
        <AtualizarQuantidadeVagasCommand,bool>
    {
        private readonly IHorarioRepository _repository;

        public AtualizarQuantidadeVagasCommandHandler(IHorarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> HandleAsync(AtualizarQuantidadeVagasCommand command)
        {
            var agenda = await _repository.ObterPorIdAsync(command.AgendaId);
            if (agenda == null) throw new AppException("Agenda não encontrada",404);
            agenda.AlterarQuantidadeVagas(command.Quantidade);
            var sucesso = await _repository.UnitOfWork.Commit();
            if (!sucesso) throw new AppException("Não foi possível atualizar a quantidade de vagas",500);
            return true;
        }
    }
}
