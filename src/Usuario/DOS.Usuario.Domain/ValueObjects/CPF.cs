using DOS.Core.Exceptions;

namespace DOS.Usuario.Domain.ValueObjects
{
    public class CPF
    {
        public string Numero { get; }

        public CPF(string valor)
        {
            EhValido(valor);
            Numero = valor;
        }
        private void EhValido(string valor)
        {
            int[] penultimoDigito = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] ultimoDigito = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            valor = valor.Trim().Replace(".", "").Replace("-", "");

            if (valor.Length != 11)
            {
                throw new DomainException("O CPF deve possuir 11 dígitos");
            }

            int totalPenultimoDigito = 0;
            for (int x = 0; x < 9; x++)
            {
                int digito = int.Parse(valor[x].ToString());
                totalPenultimoDigito += digito * penultimoDigito[x];
            }

            int resto = totalPenultimoDigito % 11;
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            if (resto.ToString() != (valor[9].ToString()))
            {
                throw new DomainException("Primeiro dígito verificador incorreto");
            }

            int totalUltimoDigito = 0;
            for (int x = 0; x < 10; x++)
            {
                int digito = int.Parse(valor[x].ToString());
                totalUltimoDigito += digito * ultimoDigito[x];
            }

            resto = totalUltimoDigito % 11;
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            if (resto.ToString() != (valor[10].ToString()))
            {
                throw new DomainException("Segundo dígito verificador incorreto");
            }
        }

    }
}