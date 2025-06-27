using System.Text.Json.Serialization;

namespace DOS.Notificacao.Application.Events.Doacao
{
    public class DoacaoFalhaEvent
    {
        

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("ocurreuEm")]
        public DateTime OcurreuEm { get; set; }
    }
}
