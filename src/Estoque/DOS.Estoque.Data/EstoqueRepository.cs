

using DOS.Core.Data;
using DOS.Estoque.Domain;
using DOS.Estoque.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace DOS.Estoque.Data
{
    public class EstoqueRepository : IEstoqueRepository
    {
        private readonly EstoqueContext _context;
        public EstoqueRepository(EstoqueContext context)
        {
            _context = context;
        }

        public IUnityOfWork UnitOfWork => _context;

        public void Adicionar(EstoqueSanguineo estoque)
        {
            _context.Estoque.Add(estoque);
        }

        public void Atualizar(EstoqueSanguineo estoque)
        {
            _context.Estoque.Update(estoque);

        }
        public async Task<EstoqueSanguineo?> ObterPorTipoAsync(TipoSanguineo tipo)
        {
            return await _context.Estoque
                .FirstOrDefaultAsync(e => e.Tipo == tipo);


        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
