using DOS.Core.Mediator.Commands;
namespace DOS.Agenda.Application.Commands
{
    public class AtualizarDataHoraCommand : ICommand<bool>
    {
        public Guid AgendaId { get; set; }
        public DateTime DataHora {  get; set; }

        public AtualizarDataHoraCommand(Guid agendaId, DateTime dataHora)
        {
            AgendaId = agendaId;
            DataHora = dataHora;
        }
    }
}
