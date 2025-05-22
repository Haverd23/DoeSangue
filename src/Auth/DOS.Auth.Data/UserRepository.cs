using DOS.Auth.Domain.Interfaces;
using DOS.Auth.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DOS.Auth.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;
        public UserRepository(UserContext context)
        {
            _context = context;
        }

        public async Task AdicionarUser(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<User> ObterPorEmail(Email email)
        {
           return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
