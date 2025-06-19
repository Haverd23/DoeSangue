using DOS.Agenda.API.DTOs;
using DOS.Agenda.Application.Commands;
using DOS.Agenda.Domain;
using DOS.Core.Mediator.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DOS.Agenda.API.Controllers
{
    //[Authorize(Roles = "Administrador")]
    [Route("api/[controller]")]
    [ApiController]
    public class AgendaController : ControllerBase
    {
        private readonly IHorarioRepository _horarioRepository;
        private readonly ICommandDispatcher _commandDispatcher;
        public AgendaController(IHorarioRepository horarioRepository,
            ICommandDispatcher commandDispatcher)
        {
            _horarioRepository = horarioRepository;
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> CriarHorario([FromBody] CriarHorarioDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Dados do horário não podem ser nulos.");
            }
            var command = new AgendaCriadaCommand(dto.DataHora,dto.NumeroVagas);
            var horarioId = await _commandDispatcher.DispatchAsync<AgendaCriadaCommand, Guid>(command);
            return CreatedAtAction(nameof(CriarHorario), new { id = horarioId }, horarioId);
        }
        [HttpDelete]
        public async Task<IActionResult> DeletarHorario([FromBody] ApagarHorarioDTO dto)
        {
            if(dto == null)
            {
                return BadRequest("Informe a agenda que será apagada");
            }
            var command = new DeletarAgendaCommand(dto.AgendaId);
            var agendaId =  await _commandDispatcher.DispatchAsync<DeletarAgendaCommand,bool>(command);
            return NoContent();
        }
    }
}
