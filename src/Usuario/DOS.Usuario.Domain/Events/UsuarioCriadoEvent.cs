using DOS.Core.DomainObjects;

namespace DOS.Usuario.Domain.Events
{
    public class UsuarioCriadoEvent : IDomainEvent
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public DateTime OcurreuEm => DateTime.UtcNow;

        public UsuarioCriadoEvent(string nome, string email)
        {
            Nome = nome;
            Email = email;
        }

    }
}
