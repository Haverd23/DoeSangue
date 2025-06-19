using DOS.Core.Mediator.Commands;

namespace DOS.Agenda.Application.Commands
{
    public class AtualizarQuantidadeVagasCommand : ICommand<bool>
    {
        public Guid AgendaId { get; set; }
        public int Quantidade { get; set; }

        public AtualizarQuantidadeVagasCommand(Guid agendaId, int quantidade)
        {
            AgendaId = agendaId;
            Quantidade = quantidade;
        }
    }
}
