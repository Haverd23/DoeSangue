using DOS.Core.DomainObjects;
using System.Diagnostics;

namespace DOS.Auth.Domain.Models
{
    public class User : Entity, IAggregateRoot
    {
        public Email Email { get; private set; }
        public string Senha { get; private set; }
        public string Role { get; private set; }

        public User(Email email, string senha, string role)
        {
            SenhaValida(senha);
            Email = email;
            Senha = senha;
            Role = role;
        }
        public void AlterarSenha(string novaSenha)
        {
            SenhaValida(novaSenha);
            Senha = novaSenha;
        }

        public void AlterarEmail(Email novoEmail)
        {
            if (novoEmail == null)
                throw new ArgumentException("Email inválido");
            Email = novoEmail;
        }

        public void SenhaValida(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
                throw new ArgumentException("Senha inválida");
            if (senha.Length < 6)
                throw new ArgumentException("Senha deve ter pelo menos 6 caracteres");
            if (!senha.Any(char.IsDigit))
                throw new ArgumentException("Senha deve conter pelo menos um número");
            if (!senha.Any(char.IsUpper))
                throw new ArgumentException("Senha deve conter pelo menos uma letra maiúscula");
            if (!senha.Any(char.IsLower))
                throw new ArgumentException("Senha deve conter pelo menos uma letra minúscula");

        }
        

    }
}
