using DOS.Core.Data;
using DOS.Doacao.Domain;
using DOS.Doacao.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace DOS.Doacao.Data
{
    public class DoacaoRepository : IDoacaoRepository
    {
        private readonly DoacaoContext _context;
        public DoacaoRepository(DoacaoContext context)
        {
            _context = context;
        }

        public IUnityOfWork UnitOfWork => _context;

        public async Task AdicionarAsync(DoacaoRegistro doacao)
        {
            await _context.Doacoes.AddAsync(doacao);
        }

        public Task AtualizarAsync(DoacaoRegistro doacao)
        {
            _context.Doacoes.Update(doacao);
            return Task.CompletedTask;
        }
        public async Task<IEnumerable<DoacaoRegistro>> ObterPorDataAsync(DateTime data)
        {
            return await _context.Doacoes.Where(d => d.DataHoraAgendada.Date == data.Date)
                .ToListAsync();
        }

        public async Task<DoacaoRegistro?> ObterPorIdAsync(Guid id)
        {
            return await _context.Doacoes.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<DoacaoRegistro>> ObterPorStatusAsync(StatusDoacao status)
        {
            return await _context.Doacoes.Where(d => d.Status == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<DoacaoRegistro>> ObterPorUsuarioAsync(Guid usuarioId)
        {
            return await _context.Doacoes.Where(d => d.UsuarioId == usuarioId)
                .ToListAsync();
        }
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
