using DOS.Core.Mediator.Commands;
using DOS.Estoque.Application.Commands;
using DOS.Estoque.Domain;
namespace DOS.Estoque.Application.CommandsHandlers
{
    public class RegistrarDoacaoEstoqueCommandHandler : ICommandHandler<RegistrarDoacaoEstoqueCommand, bool>
    {
        private readonly IEstoqueRepository _estoqueRepository;

        public RegistrarDoacaoEstoqueCommandHandler(IEstoqueRepository estoqueRepository)
        {
            _estoqueRepository = estoqueRepository;
        }

        public async Task<bool> HandleAsync(RegistrarDoacaoEstoqueCommand command)
        {
            var estoque = await _estoqueRepository.ObterPorTipoAsync(command.TipoSanguineo);

            if (estoque == null)
            {
                estoque = new EstoqueSanguineo(command.TipoSanguineo);
                estoque.RegistrarDoacao();
                _estoqueRepository.Adicionar(estoque);
            }
            else
            {
                var alterado = estoque.RegistrarDoacao();

                if (alterado)
                    _estoqueRepository.Atualizar(estoque);
            }

            return await _estoqueRepository.UnitOfWork.Commit();
        }


    }
}