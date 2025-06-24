using DOS.Auth.Application.Commands;
using DOS.Auth.Domain.Interfaces;
using DOS.Core.Mediator.Commands;

namespace DOS.Auth.Application.CommandsHandlers
{
    public class AlterarSenhaCommandHandler : ICommandHandler<AlterarSenhaCommand,bool>
    {
        private readonly IUserRepository _repository;

        public AlterarSenhaCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> HandleAsync(AlterarSenhaCommand command)
        {
            var user = await _repository.ObterPorId(command.UserId);
            if (user == null) throw new Exception("Usuário não encontrado");
            user.AlterarSenha(command.Senha);
            var sucesso = await _repository.UnitOfWork.Commit();
            if (!sucesso) throw new Exception("Não foi possível alterar a senha");
            return true;
        }
    }
}
