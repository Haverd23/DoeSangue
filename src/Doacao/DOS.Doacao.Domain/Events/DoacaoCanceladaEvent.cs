
using DOS.Core.DomainObjects;

namespace DOS.Doacao.Domain.Events
{
    public class DoacaoCanceladaEvent : IDomainEvent
    {
        public Guid DoacaoId { get; set; }
        public Guid AgendaId { get; set; }
        public DateTime OcurreuEm => DateTime.UtcNow;

        public DoacaoCanceladaEvent(Guid doacaoId, Guid agendaId)
        {
            DoacaoId = doacaoId;
            AgendaId = agendaId;
        }
    }
}
