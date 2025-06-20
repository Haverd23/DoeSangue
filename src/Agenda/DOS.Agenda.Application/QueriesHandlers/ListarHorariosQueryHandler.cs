using DOS.Agenda.Application.DTOs;
using DOS.Agenda.Application.Queries;
using DOS.Agenda.Domain;
using DOS.Core.Mediator.Queries;

namespace DOS.Agenda.Application.QueriesHandlers
{
    public class ListarHorariosQueryHandler : IQueryHandler<ListarHorariosQuery, IEnumerable<AgendaDTO>>
    {
        private readonly IHorarioRepository _repository;

        public ListarHorariosQueryHandler(IHorarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AgendaDTO>> HandleAsync(ListarHorariosQuery query)
        {
            var horarios = await _repository.ObterTodosDisponiveisAsync();

            return horarios.Select(h => new AgendaDTO
            {
                AgendaId = h.Id,
                DataHora = h.DataHora
          
            });
        }
    }
}
        

    

