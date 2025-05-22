using DOS.Auth.Domain.Models;


namespace DOS.Auth.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task AdicionarUser(User user);
        Task<User> ObterPorEmail(Email email);




    }
}
