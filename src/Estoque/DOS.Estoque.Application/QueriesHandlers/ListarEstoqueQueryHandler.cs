
using DOS.Core.Mediator.Queries;
using DOS.Estoque.Application.DTOs;
using DOS.Estoque.Application.Queries;
using DOS.Estoque.Domain;

namespace DOS.Estoque.Application.QueriesHandlers
{
    public class ListarEstoqueQueryHandler : IQueryHandler<ListarEstoqueQuery,IEnumerable<EstoqueDTO>>
    {
        private readonly IEstoqueRepository _repository;

        public ListarEstoqueQueryHandler(IEstoqueRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<EstoqueDTO>> HandleAsync(ListarEstoqueQuery command)
        {
            var estoque = await _repository.ListarEstoque();
            return estoque.Select(e => new EstoqueDTO
            {
                EstoqueId = e.Id,
                TipoSanguineo = e.Tipo.ToString(),
                Unidades = e.Unidades,
                Contador = e.ContadorDoacoes
            });
        }
    }
}
