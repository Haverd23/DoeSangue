using DOS.Core.DomainObjects;
using DOS.Core.Exceptions.DOS.Core.Exceptions;
using DOS.Doacao.Domain.Enums;
using DOS.Doacao.Domain.Events;

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
                throw new AppException("A doação só pode ser iniciada se estiver agendada",409);

            Status = StatusDoacao.EmAndamento;
        }
        public void Cancelar()
        {
            if (Status != StatusDoacao.Agendada)
                throw new AppException("A doação só pode ser cancelada se estiver agendada",409);

            Status = StatusDoacao.Cancelada;
            AddDomainEvent(new DoacaoCanceladaEvent(Id, AgendaId));
        }

        public void Finalizar()
        {
            if (Status != StatusDoacao.EmAndamento)
                throw new AppException("A doação só pode ser finalizada se estiver em andamento",409);

            if (string.IsNullOrWhiteSpace(TipoSanguineo))
                throw new AppException("Tipo sanguíneo deve ser informado para finalizar a doação",400);

            Status = StatusDoacao.Finalizada;
        }

        public void MarcarComoFalha()
        {
            if (Status != StatusDoacao.EmAndamento)
                throw new AppException("A doação só pode falhar se estiver em andamento",409);

            Status = StatusDoacao.Falha;
        }
        private void Validar()
        {
            if (DataHoraAgendada <= DateTime.Now)
                throw new AppException("A data agendada deve ser futura",400);
        }
    }
}
    

