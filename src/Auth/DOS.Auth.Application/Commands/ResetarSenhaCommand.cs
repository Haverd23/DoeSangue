using DOS.Core.Mediator.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOS.Auth.Application.Commands
{
    public class ResetarSenhaCommand : ICommand<bool>
    {
        public string Email { get; }
        public string Token { get; }
        public string NovaSenha { get; }

        public ResetarSenhaCommand(string email, string token, string novaSenha)
        {
            Email = email;
            Token = token;
            NovaSenha = novaSenha;
        }
    }
}