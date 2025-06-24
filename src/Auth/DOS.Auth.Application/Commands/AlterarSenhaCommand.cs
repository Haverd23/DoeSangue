using DOS.Core.Mediator.Commands;

namespace DOS.Auth.Application.Commands
{
    public class AlterarSenhaCommand : ICommand<bool>
    {
        public Guid UserId { get; set; }
        public string Senha {  get; set; }

        public AlterarSenhaCommand(Guid userId, string senha)
        {
            UserId = userId;
            Senha = senha;
        }
    }
}
