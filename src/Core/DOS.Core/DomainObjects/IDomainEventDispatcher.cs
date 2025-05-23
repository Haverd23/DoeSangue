namespace DOS.Core.DomainObjects
{
    public interface IDomainEventDispatcher
    {
        Task DispatchEventsAsync(IEnumerable<IDomainEvent> events);
    }
}
