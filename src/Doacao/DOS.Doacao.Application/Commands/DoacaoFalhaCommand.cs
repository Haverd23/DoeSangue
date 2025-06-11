using DOS.Core.Mediator.Commands;

namespace DOS.Doacao.Application.Commands
{
    public class DoacaoFalhaCommand: ICommand<bool>
    {
        public Guid DoacaoId { get; set; }

        public DoacaoFalhaCommand(Guid doacaoId)
        {
            DoacaoId = doacaoId;
        }

    }
}
