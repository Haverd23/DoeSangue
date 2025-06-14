using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOS.Notificacao.Application.Kafka
{
    public interface IKafkaEventHandler<TEvent>
    {
        Task HandleAsync(TEvent @event);
    }
}
