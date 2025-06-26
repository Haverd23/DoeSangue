using DOS.Auth.Domain.Events;
using DOS.Core.DomainObjects;
using DOS.Core.Exceptions;
namespace DOS.Auth.Domain.Models
{
    public class User : Entity, IAggregateRoot
    {
        public Email Email { get; private set; }
        public string Senha { get; private set; }
        public string Role { get; private set; }

        public User(string email, string senha)
        {
            SenhaValida(senha);
            Email = new Email(email);
            Senha = senha;
            Role = "User;";
            AddDomainEvent(new UserCriadoEvento(Id, Email));
        }
        protected User() { }
        public void AlterarSenha(string novaSenha)
        {
            SenhaValida(novaSenha);
            if(novaSenha == Senha) return;
            Senha = novaSenha;
            AddDomainEvent(new SenhaAlteradaEvento(Id,Email));
        }
        public void AlterarEmail(string novoEmail)
        {
            if (novoEmail == null)
                throw new DomainException("Email Inválido");
            Email = new Email(novoEmail);
            AddDomainEvent(new EmailAlteradoEvento(Id, Email));
        }
        public static void SenhaValida(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
                throw new DomainException("Senha inválida");
            if (senha.Length < 6)
                throw new DomainException("Senha deve ter pelo menos 6 caracteres");
            if (!senha.Any(char.IsDigit))
                throw new DomainException("Senha deve conter pelo menos um número");
            if (!senha.Any(char.IsUpper))
                throw new DomainException("Senha deve conter pelo menos uma letra maiúscula");
            if (!senha.Any(char.IsLower))
                throw new DomainException("Senha deve conter pelo menos uma letra minúscula");
        }      
    }
}
