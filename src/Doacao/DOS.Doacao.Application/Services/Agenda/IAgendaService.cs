using DOS.Doacao.Application.DTOs;

namespace DOS.Doacao.Application.Services.Agenda
{
    public interface IAgendaService
    {
        Task<AgendaDTO> ObterAgendaPorId(Guid Id);
    }
}
