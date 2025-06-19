using DOS.Core.Data;
using DOS.Core.Enums;

namespace DOS.Estoque.Domain
{
    public interface IEstoqueRepository : IRepository<EstoqueSanguineo>
    {
        Task<EstoqueSanguineo?> ObterPorTipoAsync(TipoSanguineo tipo);
        void Adicionar(EstoqueSanguineo estoque);
        void Atualizar(EstoqueSanguineo estoque);
    }
}
