
using DOS.Core.DomainObjects;

namespace DOS.Doacao.Application.Eventos
{
    public class DoacaoFinalizadaEvent : IDomainEvent
    {
        public Guid DoacaoId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string TipoSanguineo { get; set; }
        public DateTime OcurreuEm => DateTime.UtcNow;

        public DoacaoFinalizadaEvent(Guid doacaoId, string nome, string email,
            string tipoSanguineo)
        {
            DoacaoId = doacaoId;
            Nome = nome;
            Email = email;
            TipoSanguineo = tipoSanguineo;
        }
    }
}
