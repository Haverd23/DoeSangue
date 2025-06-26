using DOS.Agenda.Application.Kafka.Eventos;
using DOS.Agenda.Domain;

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
            if (doacao == null) throw new ApplicationException("Doaoção não encontrada");
            doacao.ReservarVaga();
            await _horarioRepository.UnitOfWork.Commit();
        }
    }
}
