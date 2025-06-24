using System.Text.RegularExpressions;


namespace DOS.Auth.Domain.Models
{
    public class Email
    {
        public string Entrada { get; private set; }

        public Email(string entrada)
        {
            if (!EhValido(entrada))
                throw new Exception("Email Inválido");

            Entrada = entrada;
        }

        private bool EhValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }
    }
}