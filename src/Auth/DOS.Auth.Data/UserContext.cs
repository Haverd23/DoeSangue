using DOS.Auth.Domain.Models;
using Microsoft.EntityFrameworkCore;
namespace DOS.Auth.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

    }
}
