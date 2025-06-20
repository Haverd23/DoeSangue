using DOS.Core.DomainObjects;

namespace DOS.Auth.Application.Eventos
{
    public class RecuperacaoDeSenhaSolicitadaEvent : IDomainEvent
    {
        public Guid UsuarioId { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime OcurreuEm => DateTime.UtcNow;

        public RecuperacaoDeSenhaSolicitadaEvent(Guid usuarioId, string email, string token)
        {
            UsuarioId = usuarioId;
            Email = email;
            Token = token;
        }
    }
}
