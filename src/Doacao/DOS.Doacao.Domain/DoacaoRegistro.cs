using DOS.Core.DomainObjects;
using DOS.Doacao.Domain.Enums;

namespace DOS.Doacao.Domain
{
    public class DoacaoRegistro : Entity, IAggregateRoot
    {
        public Guid AgendaId { get; private set; }

        public Guid UsuarioId { get; private set; }
        public string? TipoSanguineo { get; private set; }
        public DateTime DataHoraAgendada { get; private set; }
        public StatusDoacao Status { get; private set; }

        protected DoacaoRegistro() { }

        public DoacaoRegistro(Guid agendaId, Guid usuarioId, string? tipoSanguineo, DateTime dataHoraAgendada)
        {
            AgendaId = agendaId;
            UsuarioId = usuarioId;
            TipoSanguineo = tipoSanguineo;
            DataHoraAgendada = dataHoraAgendada;
            Status = StatusDoacao.Agendada;

            Validar();
        }
        public void Iniciar()
        {
            if (Status != StatusDoacao.Agendada)
                throw new Exception("A doação só pode ser iniciada se estiver agendada.");

            Status = StatusDoacao.EmAndamento;
        }

        public void Finalizar()
        {
            if (Status != StatusDoacao.EmAndamento)
                throw new Exception("A doação só pode ser finalizada se estiver em andamento.");

            if (string.IsNullOrWhiteSpace(TipoSanguineo))
                throw new Exception("Tipo sanguíneo deve ser informado para finalizar a doação.");

            Status = StatusDoacao.Finalizada;
        }

        public void MarcarComoFalha()
        {
            if (Status != StatusDoacao.EmAndamento)
                throw new Exception("A doação só pode falhar se estiver em andamento.");

            Status = StatusDoacao.Falha;
        }

        public void AtribuirTipoSanguineo(string tipoSanguineo)
        {
            if (string.IsNullOrWhiteSpace(tipoSanguineo))
                throw new Exception("Tipo sanguíneo inválido.");

            TipoSanguineo = tipoSanguineo;
        }

        private void Validar()
        {
            if (DataHoraAgendada <= DateTime.Now)
                throw new Exception("A data agendada deve ser futura.");
        }
    }
}
    

