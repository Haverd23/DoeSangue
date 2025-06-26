namespace DOS.Doacao.Application.DTOs
{
    public class UsuarioDTO
    {
        public string TipoSanguineo { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        public UsuarioDTO(string tipoSanguineo, string nome, string email)
        {
            TipoSanguineo = tipoSanguineo;
            Nome = nome;
            Email = email;
        }
    }
}
