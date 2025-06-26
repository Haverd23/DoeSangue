using DOS.Auth.Application.Commands;
using DOS.Auth.Domain.Interfaces;
using DOS.Core.Exceptions.DOS.Core.Exceptions;
using DOS.Core.Mediator.Commands;

namespace DOS.Auth.Application.CommandsHandlers
{
    public class AlterarEmailCommandHandler : ICommandHandler<AlterarEmailCommand,bool>
    {
        private readonly IUserRepository _repository;

        public AlterarEmailCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> HandleAsync(AlterarEmailCommand command)
        {
            var user = await _repository.ObterPorId(command.UserId);
            if (user == null) throw new AppException("Usuário não encontrado",404);
            user.AlterarEmail(command.Email);
            var sucesso = await _repository.UnitOfWork.Commit();
            if (!sucesso) throw new AppException("Não foi possível alterar o email",500);
            return true;
        }
    }
}
