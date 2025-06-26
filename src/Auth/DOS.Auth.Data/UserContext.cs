using DOS.Auth.Domain.Models;
using DOS.Core.Data;
using DOS.Core.Exceptions;
using DOS.Core.Exceptions.DOS.Core.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DOS.Auth.Data
{
    public class UserContext : DbContext, IUnityOfWork
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserContext).Assembly);
        }
        public async Task<bool> Commit()
        {
            try
            {
                var result = await base.SaveChangesAsync();
                return result > 0;
            }
            catch (AppException)
            {
                throw new AppException("Erro ao salvar as mudanças no banco de dados", 500);

            }
        }
    }
}
