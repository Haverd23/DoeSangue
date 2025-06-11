
using DOS.Core.DomainObjects;

namespace DOS.Doacao.Application.Eventos
{
    public class DoacaoRealizadaEvent : IDomainEvent
    {
        public Guid DoacaoId { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public DateTime OcurreuEm => DateTime.Now;

        public DoacaoRealizadaEvent(Guid doacaoId, string email, string nome)
        {
            DoacaoId = doacaoId;
            Email = email;
            Nome = nome;
        }
    }
}
