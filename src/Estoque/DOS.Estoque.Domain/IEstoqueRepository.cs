using DOS.Core.Data;
using DOS.Estoque.Domain.Enums;

namespace DOS.Estoque.Domain
{
    public interface IEstoqueRepository : IRepository<EstoqueSanguineo>
    {
        Task<EstoqueSanguineo?> ObterPorTipoAsync(TipoSanguineo tipo);
        void Adicionar(EstoqueSanguineo estoque);
        void Atualizar(EstoqueSanguineo estoque);
    }
}
