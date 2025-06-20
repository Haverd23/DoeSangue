using DOS.Core.Data;

namespace DOS.Agenda.Domain
{
    public interface IHorarioRepository : IRepository<Horario>
    {
        Task<Horario?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Horario>> ObterTodosDisponiveisAsync();
        Task<IEnumerable<Horario>> ObterPorDataAsync(DateTime data);
        Task Adicionar(Horario horario);
        Task Deletar(Guid Id);
    }
}
