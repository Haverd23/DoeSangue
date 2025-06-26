using DOS.Auth.Application.Commands;
using DOS.Auth.Application.Services.Interfaces;
using DOS.Auth.Domain.Interfaces;
using DOS.Core.Mediator.Commands;

namespace DOS.Auth.Application.CommandsHandlers
{
    public class AlterarSenhaCommandHandler : ICommandHandler<AlterarSenhaCommand,bool>
    {
        private readonly IUserRepository _repository;
        private readonly ISenhaCriptografia _criptografia;

        public AlterarSenhaCommandHandler(IUserRepository repository,
            ISenhaCriptografia criptografia)
        {
            _repository = repository;
            _criptografia = criptografia;
        }

        public async Task<bool> HandleAsync(AlterarSenhaCommand command)
        {
            var user = await _repository.ObterPorId(command.UserId);
            if (user == null) throw new ApplicationException("Usuário não encontrado");
            var senhaCriptografada = _criptografia.SenhaHash(command.Senha);
            user.AlterarSenha(senhaCriptografada);
            var sucesso = await _repository.UnitOfWork.Commit();
            if (!sucesso) throw new ApplicationException("Não foi possível alterar a senha");
            return true;
        }
    }
}
