using DOS.Agenda.Domain;
using DOS.Core.Data;
using DOS.Core.Exceptions;
using DOS.Core.Exceptions.DOS.Core.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DOS.Agenda.Data
{
    public class HorarioContext : DbContext, IUnityOfWork
    {
        public HorarioContext(DbContextOptions<HorarioContext> options) : base(options) { }


        public DbSet<Horario> Horarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HorarioContext).Assembly);
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
