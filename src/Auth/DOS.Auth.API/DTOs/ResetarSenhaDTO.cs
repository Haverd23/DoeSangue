using System.ComponentModel.DataAnnotations;

namespace DOS.Auth.API.DTOs
{
    public class ResetarSenhaDTO
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string NovaSenha { get; set; }
    }
}
