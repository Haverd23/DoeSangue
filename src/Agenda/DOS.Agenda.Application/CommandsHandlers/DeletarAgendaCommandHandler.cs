using DOS.Agenda.Application.Commands;
using DOS.Agenda.Domain;
using DOS.Core.Mediator.Commands;

namespace DOS.Agenda.Application.CommandsHandlers
{
    public class DeletarAgendaCommandHandler : ICommandHandler<DeletarAgendaCommand,bool>
    {
        private readonly IHorarioRepository _repository;

        public DeletarAgendaCommandHandler(IHorarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> HandleAsync(DeletarAgendaCommand command)
        {
            var agenda = await _repository.ObterPorIdAsync(command.AgendaId);
            if (agenda == null) throw new Exception("Agenda não encontrada");
            await _repository.Deletar(agenda.Id);
            var sucesso = await _repository.UnitOfWork.Commit();
            if (!sucesso) throw new Exception("Não foi possível deletar essa agenda");
            return true;
        }
    }
}
