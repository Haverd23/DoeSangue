using DOS.Core.Data;
using DOS.Doacao.Domain.Enums;
namespace DOS.Doacao.Domain
{
    public interface IDoacaoRepository : IRepository<DoacaoRegistro>
    {
        Task<DoacaoRegistro?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<DoacaoRegistro>> ObterPorUsuarioAsync(Guid usuarioId);
        Task<IEnumerable<DoacaoRegistro>> ObterPorStatusAsync(StatusDoacao status);
        Task<IEnumerable<DoacaoRegistro>> ObterPorDataAsync(DateTime data);
        Task AdicionarAsync(DoacaoRegistro doacao);
        Task AtualizarAsync(DoacaoRegistro doacao);
    }
}