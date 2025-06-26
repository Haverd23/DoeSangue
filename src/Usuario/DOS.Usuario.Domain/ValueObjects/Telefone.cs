using DOS.Core.Exceptions;
using System.Text.RegularExpressions;
namespace DOS.Usuario.Domain.ValueObjects
{
    public class Telefone
    {
        public string Numero { get; }

        public Telefone(string numero)
        {
            EhValido(numero);
            Numero = numero;
        }
        private void EhValido(string numero)
        {
            if (string.IsNullOrWhiteSpace(numero))
                throw new DomainException("O número não pode ser vazio");


            string limpo = Regex.Replace(numero, @"[\s\-\.\(\)]", "");

            string pattern = @"^(\+55)?\d{10,11}$";

            if (!Regex.IsMatch(limpo, pattern))
            {
                throw new DomainException("Formato do número inválido");

            }
        }
    }
}