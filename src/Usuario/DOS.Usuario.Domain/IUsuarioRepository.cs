using DOS.Core.Data;
using DOS.Usuario.Domain.ValueObjects;

namespace DOS.Usuario.Domain
{
    public interface IUsuarioRepository : IRepository<User>
    {
        Task<User> GetByCPF(CPF cpf);
        Task<User> GetById(Guid id);
        Task Adcionar(User usuario);      
    }
}
