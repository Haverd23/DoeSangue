
using Newtonsoft.Json;

namespace DOS.Estoque.Application.Kafka.Eventos
{
    public class DoacaoFinalizadaEvent
    {
        [JsonProperty("tipoSanguineo")]
        public string TipoSanguineo { get; set; }
    }
}
