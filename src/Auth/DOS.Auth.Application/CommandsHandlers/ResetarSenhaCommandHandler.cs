using DOS.Auth.Application.Commands;
using DOS.Auth.Application.Services.Interfaces;
using DOS.Auth.Domain.Interfaces;
using DOS.Auth.Domain.Models;
using DOS.Core.Data;
using DOS.Core.Mediator.Commands;

namespace DOS.Auth.Application.CommandsHandlers
{
    public class ResetarSenhaCommandHandler : ICommandHandler<ResetarSenhaCommand,bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenJWT _token;
        private readonly ISenhaCriptografia _criptografia;

        public ResetarSenhaCommandHandler(IUserRepository userRepository, ITokenJWT token,
            ISenhaCriptografia criptografia)
        {
            _userRepository = userRepository;
            _token = token;
            _criptografia = criptografia;
        }

        public async Task<bool> HandleAsync(ResetarSenhaCommand command)
        {
            User.SenhaValida(command.NovaSenha);
            var email = new Email(command.Email);
            var user = await _userRepository.ObterPorEmail(email);

            if (user is null)
                throw new Exception("Usuário não encontrado.");

            var tokenValido = _token.ValidarTokenRecuperacaoSenha(command.Token, command.Email);
            if (!tokenValido)
                throw new Exception("Token inválido ou expirado.");

            var senhaHash =  _criptografia.SenhaHash(command.NovaSenha);


            user.AlterarSenha(senhaHash); 

            var sucesso = await _userRepository.UnitOfWork.Commit();
            if (!sucesso) throw new Exception("Não foi possível alterar a senha");

            return true;

           
        }
    }
}
    

