using DOS.Core.Enums;
using DOS.Core.Mediator.Commands;
using DOS.Estoque.Application.Commands;
using DOS.Estoque.Domain;

namespace DOS.Estoque.Application.CommandsHandlers
{
    public class RetirarUnidadeSanguineaCommandHandler : ICommandHandler
        <RetirarUnidadeSanguineaCommand,bool>
    {
        private readonly IEstoqueRepository _repository;

        public RetirarUnidadeSanguineaCommandHandler(IEstoqueRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> HandleAsync(RetirarUnidadeSanguineaCommand command)
        {
            var tipoSanguineo = (TipoSanguineo)Enum.Parse(typeof(TipoSanguineo),
                command.TipoSanguineo, ignoreCase: true);

            var tipo = await _repository.ObterPorTipoAsync(tipoSanguineo);

            tipo!.RetirarUnidade(command.Quantidade);

            var sucesso = await _repository.UnitOfWork.Commit();
            if (!sucesso) throw new ApplicationException("Não foi possível retirar unidade sanguínea");
            return true;

           
        }
    }
}
