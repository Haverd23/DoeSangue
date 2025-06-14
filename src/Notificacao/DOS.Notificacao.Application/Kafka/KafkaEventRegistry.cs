using DOS.Notificacao.Application.Events.Usuario;
using DOS.Notificacao.Application.EventsHandlers.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DOS.Notificacao.Application.Kafka
{
    public class KafkaEventRegistry
    {
        private readonly Dictionary<string, (Type eventType, Type handlerType)> _mapeamento = new();

        public KafkaEventRegistry()
        {
            Register<UsuarioCriadoEvent, UsuarioCriadoEventHandler>("UsuarioCriadoEvent");
        }

        private void Register<TEvent, THandler>(string topic)
        {
            _mapeamento[topic] = (typeof(TEvent), typeof(THandler));
        }

        public bool TryGetTypes(string topic, out Type eventType, out Type handlerType)
        {
            if (_mapeamento.TryGetValue(topic, out var types))
            {
                eventType = types.eventType;
                handlerType = types.handlerType;
                return true;
            }

            eventType = null!;
            handlerType = null!;
            return false;
        }

        public object? DeserializeEvent(string message, Type eventType)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message), "Mensagem está vazia ou nula.");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize(message, eventType, options);
        }

        public IEnumerable<string> GetTopics()
        {
            return _mapeamento.Keys;
        }

    }
}