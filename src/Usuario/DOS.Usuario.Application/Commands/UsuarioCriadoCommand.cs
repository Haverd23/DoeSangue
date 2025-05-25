using DOS.Core.Mediator.Commands;
using DOS.Usuario.Domain.Enums;

namespace DOS.Usuario.Application.Commands
{
    public class UsuarioCriadoCommand : ICommand<Guid>
    {
        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public string Telefone { get; private set; }
        public string Email { get; private set; }
        public TipoSanguineo TipoSanguineo { get; private set; }

        public UsuarioCriadoCommand(string nome,string email,
            string cpf, string telefone, TipoSanguineo tipoSanguineo)
        {
            Nome = nome;
            Email = email;
            CPF = cpf;
            Telefone = telefone;
            TipoSanguineo = tipoSanguineo;
        }
        public void SetEmail(string email)
        {
            Email = email;
        }
    }
}
