namespace DOS.Core.Mediator.Commands
{
    public interface ICommandHandler<TComand, TResult>
        where TComand : ICommand<TResult>
    {
        Task<TResult> HandleAsync(TComand command);
    }
}
