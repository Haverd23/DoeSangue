using DOS.Agenda.Application.Commands;
using DOS.Agenda.Domain;
using DOS.Core.Exceptions;
using DOS.Core.Exceptions.DOS.Core.Exceptions;
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
            if (agenda == null) throw new AppException("Agenda não encontrada", 404);
            agenda.AlterarDataHora(command.DataHora);
            var sucesso = await _repository.UnitOfWork.Commit();
            if (!sucesso) throw new AppException("Não foi possível atualizar data e hora", 500);
            return true;

        }
    }
}
