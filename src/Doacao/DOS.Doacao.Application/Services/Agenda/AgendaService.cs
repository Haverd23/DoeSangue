using DOS.Agenda.Domain;
using DOS.Core.Exceptions.DOS.Core.Exceptions;
using DOS.Doacao.Application.DTOs;

namespace DOS.Doacao.Application.Services.Agenda
{
    public class AgendaService : IAgendaService
    {
        private readonly IHorarioRepository _horarioRepository;

        public AgendaService(IHorarioRepository horarioRepository)
        {
            _horarioRepository = horarioRepository;
        }

        public async Task<AgendaDTO> ObterAgendaPorId(Guid Id)
        {
            var agenda = await _horarioRepository.ObterPorIdAsync(Id);
            if (agenda == null) throw new AppException("Agenda não encontrada", 404);

            return new AgendaDTO(agenda.Id, agenda.DataHora);
        }
    }
}
