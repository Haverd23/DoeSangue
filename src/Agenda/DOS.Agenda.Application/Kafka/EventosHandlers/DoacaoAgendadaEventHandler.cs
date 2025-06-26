using DOS.Agenda.Application.Kafka.Eventos;
using DOS.Agenda.Domain;
using DOS.Core.Exceptions;
using DOS.Core.Exceptions.DOS.Core.Exceptions;

namespace DOS.Agenda.Application.Kafka.EventosHandlers
{
    public class DoacaoAgendadaEventHandler
    {
        private readonly IHorarioRepository _horarioRepository;

        public DoacaoAgendadaEventHandler(IHorarioRepository horarioRepository)
        {
            _horarioRepository = horarioRepository;
        }

        public async Task HandleAsync(DoacaoAgendadaEvent evento)
        {
            var doacao = await _horarioRepository.ObterPorIdAsync(evento.AgendaId);
            if (doacao == null) throw new AppException("Doaoção não encontrada",404);
            doacao.ReservarVaga();
            await _horarioRepository.UnitOfWork.Commit();
        }
    }
}
