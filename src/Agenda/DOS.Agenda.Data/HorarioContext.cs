using DOS.Agenda.Domain;
using DOS.Core.Data;
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
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar as mudanças no banco de dados", ex);

            }
        }
    }
}
