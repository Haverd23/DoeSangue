
using DOS.Core.Mediator.Commands;

namespace DOS.Agenda.Application.Commands
{
    public class DeletarAgendaCommand : ICommand<bool>
    {
        public Guid AgendaId { get; set; }

        public DeletarAgendaCommand(Guid agendaId)
        {
            AgendaId = agendaId;
        }
    }
}
