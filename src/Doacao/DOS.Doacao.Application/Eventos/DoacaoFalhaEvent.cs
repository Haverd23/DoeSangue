using DOS.Core.DomainObjects;

namespace DOS.Doacao.Application.Eventos
{
    internal class DoacaoFalhaEvent: IDomainEvent
    {
        public Guid DoacaoId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime OcurreuEm => DateTime.UtcNow;

        public DoacaoFalhaEvent(Guid doacaoId, string nome, string email)
        {
            DoacaoId = doacaoId;
            Nome = nome;
            Email = email;
        }
    }
}