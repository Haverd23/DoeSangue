using DOS.Core.Mediator.Commands;
using DOS.Estoque.Domain.Enums;
namespace DOS.Estoque.Application.Commands
{
    public class RegistrarDoacaoEstoqueCommand : ICommand<bool>
    {
        public TipoSanguineo TipoSanguineo { get; }

        public RegistrarDoacaoEstoqueCommand(TipoSanguineo tipoSanguineo)
        {
            TipoSanguineo = tipoSanguineo;
        }
    }
}
    