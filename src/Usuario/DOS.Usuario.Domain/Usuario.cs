using DOS.Core.DomainObjects;
using DOS.Usuario.Domain.Enums;
using DOS.Usuario.Domain.ValueObjects;

namespace DOS.Usuario.Domain
{
    public class Usuario : Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public CPF CPF { get; private set; }
        public Telefone Telefone { get; private set; }
        public TipoSanguineo? TipoSanguineo { get; private set; }

        public Usuario(string nome, string email, string cpf, string telefone, TipoSanguineo? tipoSanguineo)
        {
            ValidarNome(nome);
            Nome = nome;
            Email = email;
            CPF = new CPF(cpf);
            Telefone = new Telefone(telefone);
            TipoSanguineo = tipoSanguineo;
        }
        public void AlterTelefone(string telefone)
        {
            Telefone = new Telefone(telefone);
        }
        public void AlterTipoSanguineo(TipoSanguineo tipoSanguineo)
        {
            TipoSanguineo = tipoSanguineo;
        }
        private void ValidarNome(string nome)
        {
            if (nome.Length < 3)
            {
                throw new Exception("O nome deve ter pelo menos 3 caracteres");
            }
        }
    }
}
