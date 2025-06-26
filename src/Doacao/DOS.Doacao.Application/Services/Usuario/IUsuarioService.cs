using DOS.Doacao.Application.DTOs;

namespace DOS.Doacao.Application.Services.Usuario
{
    public interface IUsuarioService
    {
        Task<UsuarioDTO> ObterUsuarioPorId(Guid id);
    }
}