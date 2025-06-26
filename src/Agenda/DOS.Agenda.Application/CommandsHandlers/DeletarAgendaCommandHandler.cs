using DOS.Agenda.Application.Commands;
using DOS.Agenda.Domain;
using DOS.Core.Exceptions;
using DOS.Core.Exceptions.DOS.Core.Exceptions;
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
            if (agenda == null) throw new AppException("Agenda não encontrada",404);
            await _repository.Deletar(agenda.Id);
            var sucesso = await _repository.UnitOfWork.Commit();
            if (!sucesso) throw new AppException("Não foi possível deletar essa agenda",500);
            return true;
        }
    }
}
