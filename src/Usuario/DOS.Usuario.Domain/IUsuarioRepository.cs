using DOS.Core.Data;

namespace DOS.Usuario.Domain
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario> GetByEmail(string email);
        Task<Usuario> GetById(Guid id);
        Task Adcionar(Usuario usuario);      
    }
}
