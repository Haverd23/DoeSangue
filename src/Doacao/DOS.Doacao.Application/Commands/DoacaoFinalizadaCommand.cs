
using DOS.Core.Mediator.Commands;

namespace DOS.Doacao.Application.Commands
{
    public class DoacaoFinalizadaCommand : ICommand<bool>
    {
        public Guid DoacaoId { get; set; }

        public DoacaoFinalizadaCommand(Guid doacaoId)
        {
            DoacaoId = doacaoId;
        }
    }
}
