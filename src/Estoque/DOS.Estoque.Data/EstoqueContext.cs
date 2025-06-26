using DOS.Core.Data;
using DOS.Core.Exceptions.DOS.Core.Exceptions;
using DOS.Estoque.Domain;
using Microsoft.EntityFrameworkCore;

namespace DOS.Estoque.Data
{
    public class EstoqueContext : DbContext, IUnityOfWork
    {
        public EstoqueContext(DbContextOptions<EstoqueContext> options) : base(options) { }

        public DbSet<EstoqueSanguineo> Estoque { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EstoqueContext).Assembly);
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
