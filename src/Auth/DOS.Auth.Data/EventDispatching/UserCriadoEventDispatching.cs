using DOS.Core.DomainObjects;


namespace DOS.Auth.Data.EventDispatching
{
    public class UserCriadoEventDispatching : IDomainEventDispatcher
    {
        public Task DispatchEventsAsync(IEnumerable<IDomainEvent> events)
        {
            return Task.CompletedTask;
        }
    }
}
