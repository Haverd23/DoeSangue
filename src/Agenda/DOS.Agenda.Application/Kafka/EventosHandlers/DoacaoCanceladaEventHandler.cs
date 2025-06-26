using DOS.Agenda.Application.Kafka.Eventos;
using DOS.Agenda.Domain;
using DOS.Core.Exceptions;
using DOS.Core.Exceptions.DOS.Core.Exceptions;

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
            if (agenda == null) throw new AppException("Agenda não encontrada",404);
            agenda.LiberarVaga();
            await _horarioRepository.UnitOfWork.Commit();

        }
    }
}
