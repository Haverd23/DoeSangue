using DOS.Core.Mediator.Commands;

namespace DOS.Auth.Application.Commands
{
    public class AlterarEmailCommand : ICommand<bool>
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }

        public AlterarEmailCommand(Guid userId, string email)
        {
            UserId = userId;
            Email = email;
        }
    }
}
