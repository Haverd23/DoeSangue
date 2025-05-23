namespace DOS.Core.Mediator.Commands
{
    public interface ICommandDispatcher
    {
        Task<TResult> DispatchAsync<TCommand, TResult>(TCommand command)
            where TCommand : ICommand<TResult>;
    }
}
