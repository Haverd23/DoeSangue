using DOS.Core.Data;
using DOS.Usuario.Domain;
using Microsoft.EntityFrameworkCore;
namespace DOS.Usuario.Data
{
    public class UsuarioContext : DbContext, IUnityOfWork
    {
        public UsuarioContext(DbContextOptions<UsuarioContext> options) : base(options){}

        public DbSet<User> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UsuarioContext).Assembly);
        }
        public async Task<bool> Commit()
        {
            try
            {
                var result = await base.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar as mudanças no banco de dados", ex);

            }
        }
    }
}
