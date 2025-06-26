using DOS.Core.Mediator.Commands;

namespace DOS.Doacao.Application.Commands
{
    public class AgendarDoacaoCommand : ICommand<Guid>
    {
        public Guid AgendaId { get; set; }
        public Guid UserId { get; set; }
        public string TipoSanguineo { get; set; }
        public DateTime DataHoraAgendada { get; private set; }

        public AgendarDoacaoCommand(Guid agendaId)
        {
            AgendaId = agendaId;
        }
    }
}