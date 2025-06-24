using DOS.Auth.Domain.Models;
using DOS.Core.Mediator.Commands;
namespace DOS.Auth.Application.Commands
{
    public class UsuarioCriadoCommand : ICommand<Guid>
    {
        public string Email { get; private set; }
        public string Senha { get; private set; }

        public UsuarioCriadoCommand(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }
    }
}
