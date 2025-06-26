using DOS.Auth.Domain.Events;
using DOS.Core.DomainObjects;
using DOS.Core.Exceptions.DOS.Core.Exceptions;
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
                throw new AppException("Email Inválido",400);
            Email = new Email(novoEmail);
            AddDomainEvent(new EmailAlteradoEvento(Id, Email));
        }
        public void AlterarRole(string role)
        {
            if (role == null) throw new AppException("Role não pode ser vazia", 400);
            if (role != "Administrador" && role != "User") throw new AppException("Role inválida",400);
            Role = role;
        }
        public static void SenhaValida(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
                throw new AppException("Senha inválida",400);
            if (senha.Length < 6)
                throw new AppException("Senha deve ter pelo menos 6 caracteres",400);
            if (!senha.Any(char.IsDigit))
                throw new AppException("Senha deve conter pelo menos um número", 400);
            if (!senha.Any(char.IsUpper))
                throw new AppException("Senha deve conter pelo menos uma letra maiúscula", 400);
            if (!senha.Any(char.IsLower))
                throw new AppException("Senha deve conter pelo menos uma letra minúscula", 400);
        }      
    }
}
