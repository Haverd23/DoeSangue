using DOS.Core.DomainObjects;
namespace DOS.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnityOfWork UnitOfWork { get; }
    }
}
