using DOS.Core.Data;

namespace DOS.Usuario.Domain
{
    public interface IUsuarioRepository : IRepository<User>
    {
        Task<User> GetByEmail(string email);
        Task<User> GetById(Guid id);
        Task Adcionar(User usuario);      
    }
}
