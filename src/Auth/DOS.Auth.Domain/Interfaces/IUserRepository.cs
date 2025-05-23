using DOS.Auth.Domain.Models;
using DOS.Core.Data;


namespace DOS.Auth.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task AdicionarUser(User user);
        Task<User> ObterPorEmail(Email email);
    }
}
