using DOS.Agenda.Domain;
using DOS.Core.Data;
using DOS.Core.Exceptions.DOS.Core.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DOS.Agenda.Data
{
    public class HorarioRepository : IHorarioRepository
    {
        private readonly HorarioContext _context;
        public HorarioRepository(HorarioContext context)
        {
            _context = context;
        }

        public IUnityOfWork UnitOfWork => _context;

        public async Task Adicionar(Horario horario)
        {
            await _context.Horarios.AddAsync(horario);
        }
        public async Task<IEnumerable<Horario>> ObterPorDataAsync(DateTime data)
        {
            return await _context.Horarios
                .Where(h => h.DataHora.Date == data.Date)
                .ToListAsync();
        }

        public async Task<Horario?> ObterPorIdAsync(Guid id)
        {
            return await _context.Horarios
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<IEnumerable<Horario>> ObterTodosDisponiveisAsync()
        {
            return await _context.Horarios
                .Where(h => h.VagasOcupadas < h.VagasTotais).ToListAsync();
        }

        public async Task Deletar(Guid id)
        {
            var agenda = await ObterPorIdAsync(id);
            if (agenda == null)
                throw new AppException("Agenda não encontrada", 500);
            _context.Remove(agenda);
        }
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
