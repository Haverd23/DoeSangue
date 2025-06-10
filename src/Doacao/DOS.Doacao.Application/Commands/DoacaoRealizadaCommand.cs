
using DOS.Core.Mediator.Commands;
using System.Windows.Input;

namespace DOS.Doacao.Application.Commands
{
    public class DoacaoRealizadaCommand : ICommand<bool>
    {
        public Guid DoacaoId { get; set; }


        public DoacaoRealizadaCommand(Guid doacaoId)
        {
            DoacaoId = doacaoId;
        }
    }
}
