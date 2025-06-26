using DOS.Agenda.Application.Commands;
using DOS.Agenda.Domain;
using DOS.Core.Mediator.Commands;

namespace DOS.Agenda.Application.CommandsHandlers
{
    public class AtualizarDataHoraCommandHandler : ICommandHandler<AtualizarDataHoraCommand,
        bool>
    {
        private readonly IHorarioRepository _repository;

        public AtualizarDataHoraCommandHandler(IHorarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> HandleAsync(AtualizarDataHoraCommand command)
        {
            var agenda = await _repository.ObterPorIdAsync(command.AgendaId);
            if (agenda == null) throw new ApplicationException("Agenda não encontrada");
            agenda.AlterarDataHora(command.DataHora);
            var sucesso = await _repository.UnitOfWork.Commit();
            if (!sucesso) throw new ApplicationException("Não foi possível atualizar data e hora");
            return true;

        }
    }
}
