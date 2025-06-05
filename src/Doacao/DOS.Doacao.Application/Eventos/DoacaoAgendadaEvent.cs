using DOS.Core.DomainObjects;

namespace DOS.Doacao.Application.Eventos
{
    public class DoacaoAgendadaEvent : IDomainEvent
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public Guid DoacaoId { get; private set; }
        public DateTime DataHoraAgendada { get; private set; }
        public DateTime OcurreuEm => DateTime.UtcNow;
        public DoacaoAgendadaEvent(string nome, string email, Guid doacaoId, DateTime dataHoraAgendada)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Email = email;
            DoacaoId = doacaoId;
            DataHoraAgendada = dataHoraAgendada;
        }
    }
}
