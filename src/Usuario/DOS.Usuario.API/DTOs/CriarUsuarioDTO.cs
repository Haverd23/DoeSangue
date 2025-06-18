using DOS.Usuario.Domain.Enums;

namespace DOS.Usuario.API.DTOs
{
    public class CriarUsuarioDTO
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string TipoSanguineo { get; set; }
    }
}
