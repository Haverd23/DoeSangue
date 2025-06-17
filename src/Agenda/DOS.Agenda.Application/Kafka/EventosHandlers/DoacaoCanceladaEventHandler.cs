using DOS.Agenda.Application.Kafka.Eventos;
using DOS.Agenda.Domain;

namespace DOS.Agenda.Application.Kafka.EventosHandlers
{
    public class DoacaoCanceladaEventHandler
    {
        private readonly IHorarioRepository _horarioRepository;

        public DoacaoCanceladaEventHandler(IHorarioRepository horarioRepository)
        {
            _horarioRepository = horarioRepository;
        }

        public async Task HandleAsync(DoacaoCanceladaEvent evento)
        {
            var agenda = await _horarioRepository.ObterPorIdAsync(evento.AgendaId);
            if (agenda == null) throw new Exception("Agenda não encontrada.");
            agenda.LiberarVaga();
            await _horarioRepository.UnitOfWork.Commit();

        }
    }
}
