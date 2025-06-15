using System.Text.Json.Serialization;
namespace DOS.Notificacao.Application.Events.Doacao
{
    public class DoacaoAgendadaEvent
    {
        [JsonPropertyName("name")]
        public string Nome { get; private set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("dataHoraAgendada")]
        public DateTime DataHoraAgendada { get; set; }

        [JsonPropertyName("ocurreuEm")]
        public DateTime OcurreuEm { get; set; }
    }
}
