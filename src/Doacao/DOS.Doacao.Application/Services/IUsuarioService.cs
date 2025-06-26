
using DOS.Doacao.Application.DTOs;

namespace DOS.Doacao.Application.Services
{
    public interface IUsuarioService
    {
        Task<UsuarioDTO> ObterUsuarioPorId(Guid id);
    }
}