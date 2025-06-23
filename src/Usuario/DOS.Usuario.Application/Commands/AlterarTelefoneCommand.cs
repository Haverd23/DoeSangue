
using DOS.Core.Mediator.Commands;

namespace DOS.Usuario.Application.Commands
{
    public class AlterarTelefoneCommand : ICommand<bool>
    {
        public Guid UserId { get; set; }
        public string Telefone { get; set; }

        public AlterarTelefoneCommand(string telefone, Guid userId)
        {
            Telefone = telefone;
            UserId = userId;
        }
    }
}
