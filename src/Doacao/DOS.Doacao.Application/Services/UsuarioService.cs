using DOS.Core.Exceptions.DOS.Core.Exceptions;
using DOS.Doacao.Application.DTOs;
using DOS.Usuario.Domain;


namespace DOS.Doacao.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<UsuarioDTO> ObterUsuarioPorId(Guid id)
        {
            var usuario = await _usuarioRepository.GetById(id);
            if (usuario == null) throw new AppException("Usuário não encontrado", 404);

            return new UsuarioDTO(usuario.TipoSanguineo.ToString(),
                usuario.Nome,usuario.Email);
        }
    }
}