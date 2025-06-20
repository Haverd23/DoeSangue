﻿using DOS.Core.Mediator.Commands;

namespace DOS.Usuario.Application.Commands
{
    public class UsuarioCriadoCommand : ICommand<Guid>
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public string Telefone { get; private set; }
        public string Email { get; private set; }
        public string TipoSanguineo { get; private set; }

        public UsuarioCriadoCommand(string nome,
            string cpf, string telefone, string tipoSanguineo)
        {
            Nome = nome;
            CPF = cpf;
            Telefone = telefone;
            TipoSanguineo = tipoSanguineo;
        }
        public void SetEmail(string email)
        {
            Email = email;
        }
        public void SetId(Guid id)
        {
            Id = id;
        }
    }
}
