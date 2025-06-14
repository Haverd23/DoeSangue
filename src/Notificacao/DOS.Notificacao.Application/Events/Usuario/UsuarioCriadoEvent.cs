using System.Text.Json.Serialization;

namespace DOS.Notificacao.Application.Events.Usuario
{
    public class UsuarioCriadoEvent
    {
        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("ocurreuEm")]
        public DateTime OcurreuEm { get; set; }
    }
}
