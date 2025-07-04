﻿using DOS.Core.DomainObjects;
using DOS.Core.Enums;
using DOS.Core.Exceptions.DOS.Core.Exceptions;
using DOS.Usuario.Domain.Events;
using DOS.Usuario.Domain.ValueObjects;

namespace DOS.Usuario.Domain
{
    public class User : Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public CPF CPF { get; private set; }
        public Telefone Telefone { get; private set; }
        public TipoSanguineo? TipoSanguineo { get; private set; }

        protected User() { }

        public User(Guid id,string nome, string email, string cpf, string telefone,
            TipoSanguineo? tipoSanguineo) : base(id)
        {
            ValidarNome(nome);
            Nome = nome;
            Email = email;
            CPF = new CPF(cpf);
            Telefone = new Telefone(telefone);
            TipoSanguineo = tipoSanguineo;
            AddDomainEvent(new UsuarioCriadoEvent(nome,email));
        }
        public void AlterTelefone(string telefone)
        {
            Telefone = new Telefone(telefone);
        }
        public void AlterarTipoSanguineo(TipoSanguineo tipoSanguineo)
        {
            TipoSanguineo = tipoSanguineo;
        }
        private void ValidarNome(string nome)
        {
            if (nome.Length < 3)
            {
                throw new AppException("O nome deve ter pelo menos 3 caracteres",400);
            }
        }
    }
}
