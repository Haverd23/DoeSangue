using DOS.Auth.Domain.Models;
using DOS.Core.DomainObjects;
namespace DOS.Auth.Domain.Events
{
    public class SenhaAlteradaEvento : IDomainEvent
    {
        public Guid UserId { get; private set; }
        public Email Email { get; private set; }
        public DateTime OcurreuEm => DateTime.UtcNow;

        public SenhaAlteradaEvento(Guid userId, Email email)
        {
            UserId = userId;
            Email = email;
        }
    }
}
