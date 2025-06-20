using DOS.Core.Mediator.Commands;
using DOS.Core.Mediator.Queries;
using Microsoft.Extensions.DependencyInjection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DOS.Agenda.Data.Mediator
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            var handler = _serviceProvider.GetService<IQueryHandler<TQuery, TResult>>();
            if (handler == null)
                throw new InvalidOperationException($"Handler não encontrado para a query: {typeof(TQuery).Name}");

            return await handler.HandleAsync(query);
        }

    }
}
