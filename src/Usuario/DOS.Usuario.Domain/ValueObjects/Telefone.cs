using DOS.Core.Exceptions;
using DOS.Core.Exceptions.DOS.Core.Exceptions;
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
                throw new AppException("O número não pode ser vazio", 400);


            string limpo = Regex.Replace(numero, @"[\s\-\.\(\)]", "");

            string pattern = @"^(\+55)?\d{10,11}$";

            if (!Regex.IsMatch(limpo, pattern))
            {
                throw new AppException("Formato do número inválido", 400);

            }
        }
    }
}