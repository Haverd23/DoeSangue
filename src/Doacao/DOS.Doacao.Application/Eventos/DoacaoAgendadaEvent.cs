using DOS.Core.DomainObjects;

namespace DOS.Doacao.Application.Eventos
{
    public class DoacaoAgendadaEvent : IDomainEvent
    {
        public Guid AgendaId { get;  set; }
        public Guid Id { get;  set; }
        public string Nome { get;  set; }
        public string Email { get;  set; }
        public Guid DoacaoId { get;  set; }
        public DateTime DataHoraAgendada { get;  set; }
        public DateTime OcurreuEm => DateTime.UtcNow;
        public DoacaoAgendadaEvent(string nome, string email,
            Guid doacaoId, DateTime dataHoraAgendada, Guid agendaId)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Email = email;
            DoacaoId = doacaoId;
            DataHoraAgendada = dataHoraAgendada;
            AgendaId = agendaId;
        }
    }
}
