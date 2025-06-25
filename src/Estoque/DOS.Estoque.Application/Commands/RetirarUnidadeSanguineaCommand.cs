using DOS.Core.Mediator.Commands;

namespace DOS.Estoque.Application.Commands
{
    public class RetirarUnidadeSanguineaCommand : ICommand<bool>
    {
        public string TipoSanguineo { get; set; }
        public int Quantidade { get; set; }

        public RetirarUnidadeSanguineaCommand(string tipoSanguineo, int quantidade)
        {
            TipoSanguineo = tipoSanguineo;
            Quantidade = quantidade;
        }
    }
}
