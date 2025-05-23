namespace DOS.Core.Mediator.Queries
{
    public interface IQueryDispatcher
    {
        Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>;
    }
}
