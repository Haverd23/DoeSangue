using DOS.Auth.Application.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace DOS.Auth.Application.Services
{
    public class SenhaCriptografia : ISenhaCriptografia
    {
        public string SenhaHash(string password)
        {
            using (var hmac = new HMACSHA256())
            {
                var salt = hmac.Key;
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
            }
        }
        public bool VerificarSenha(string password, string storedHash)
        {
            var parts = storedHash.Split(':');
            var salt = Convert.FromBase64String(parts[0]);
            var storedPasswordHash = Convert.FromBase64String(parts[1]);
            using (var hmac = new HMACSHA256(salt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedPasswordHash);
            }
        }
    }
}
