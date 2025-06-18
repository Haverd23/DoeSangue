using DOS.Core.Mediator.Commands;

namespace DOS.Usuario.Application.Commands
{
    public class AlterarTipoSanguineoCommand : ICommand<bool>
    {
        public string CPF { get; set; }
        public string TipoSanguineo { get; set; }

        public AlterarTipoSanguineoCommand(string cPF, string tipoSanguineo)
        {
            CPF = cPF;
            TipoSanguineo = tipoSanguineo;
        }
    }
}
