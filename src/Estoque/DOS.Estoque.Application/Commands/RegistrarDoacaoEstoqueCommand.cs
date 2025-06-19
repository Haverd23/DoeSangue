using DOS.Core.Enums;
using DOS.Core.Mediator.Commands;
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
    