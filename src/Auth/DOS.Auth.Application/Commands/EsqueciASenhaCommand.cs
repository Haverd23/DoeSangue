using DOS.Core.Mediator.Commands;

namespace DOS.Auth.Application.Commands
{
    public class EsqueciASenhaCommand : ICommand<bool>
    {
        public string Email { get; set; }

        public EsqueciASenhaCommand(string email)
        {
            Email = email;
        }
    }
}
