using DOS.Auth.Application.Services.Interfaces;
using DOS.Auth.Domain.Interfaces;
using DOS.Auth.Domain.Models;
using DOS.Core.Exceptions.DOS.Core.Exceptions;

namespace DOS.Auth.Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly ITokenJWT _tokenJWT;
        private readonly ISenhaCriptografia _senhaCriptografia;
        private readonly IUserRepository _userRepository;

        public LoginService(ITokenJWT tokenJWT, ISenhaCriptografia senhaCriptografia,
            IUserRepository userRepository)
        {
            _tokenJWT = tokenJWT;
            _senhaCriptografia = senhaCriptografia;
            _userRepository = userRepository;
        }

        public async Task<string> Autenticar(Email email, string senha)
        {
            var usuario = await _userRepository.ObterPorEmail(email);
            if (usuario == null) throw new AppException("Esse usuário não existe", 404);
            if (!_senhaCriptografia.VerificarSenha(senha, usuario.Senha))
                throw new AppException("Usuário ou senha inválidos",400);

            return await _tokenJWT.GerarToken(usuario);
        }
    }
}