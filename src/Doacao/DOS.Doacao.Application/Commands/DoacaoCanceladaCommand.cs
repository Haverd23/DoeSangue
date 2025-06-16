using DOS.Core.Mediator.Commands;
namespace DOS.Doacao.Application.Commands
{
    public class DoacaoCanceladaCommand : ICommand<bool>
    {
        public Guid DoacaoId { get; set; }

        public DoacaoCanceladaCommand(Guid doacaoId)
        {
            DoacaoId = doacaoId;
        }
    }
}
