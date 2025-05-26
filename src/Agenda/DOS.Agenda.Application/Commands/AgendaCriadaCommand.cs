using DOS.Core.Mediator.Commands;

namespace DOS.Agenda.Application.Commands
{
    public class AgendaCriadaCommand : ICommand<Guid>
    {
        public DateTime DataHora { get; set; }
        public int VagasTotais { get; set; }

        public AgendaCriadaCommand(DateTime dataHora, int vagasTotais)
        {
            DataHora = dataHora;
            VagasTotais = vagasTotais;
        }
    }
}
