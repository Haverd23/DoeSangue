using DOS.Agenda.API.DTOs;
using DOS.Agenda.Application.Commands;
using DOS.Agenda.Application.DTOs;
using DOS.Agenda.Application.Queries;
using DOS.Core.Mediator.Commands;
using DOS.Core.Mediator.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DOS.Agenda.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AgendaController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public AgendaController(ICommandDispatcher commandDispatcher,
            IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
        [HttpDelete]
        public async Task<IActionResult> DeletarHorario([FromBody] ApagarHorarioDTO dto)
        {
            if(dto == null)
            {
                return BadRequest("Informe a agenda que será apagada");
            }
            var command = new DeletarAgendaCommand(dto.AgendaId);
            var agendaId =  await _commandDispatcher.DispatchAsync<DeletarAgendaCommand,
                bool>(command);
            return NoContent();
        }
        [Authorize(Roles = "Administrador")]
        [HttpPut("atualizar/horario")]
        public async Task<IActionResult> AtualizarHorario([FromBody] AlterarHorarioDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Informe a agenda que será apagada");
            }
            var command = new AtualizarDataHoraCommand(dto.AgendaId, dto.Horario);
            var horarioId = await _commandDispatcher.DispatchAsync<AtualizarDataHoraCommand,
                bool>(command);
            return NoContent();
        }
        [Authorize(Roles = "Administrador")]
        [HttpPut("atualizar/quantidade/vagas")]
        public async Task<IActionResult> AtualizarQuantidadeVagas([FromBody]
        AlterarQuantidadeVagasDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Informe a agenda que será atualizada");
            }
            var command = new AtualizarQuantidadeVagasCommand(dto.AgendaId,dto.Quantidade);
            var agendaId = await _commandDispatcher.DispatchAsync<AtualizarQuantidadeVagasCommand,
                bool>(command);
            return NoContent();
        }
        [HttpGet("horarios")]
        public async Task<IActionResult> ListarHorarios()
        {
            var query = new ListarHorariosQuery();

            var horarios = await _queryDispatcher.DispatchAsync<ListarHorariosQuery, IEnumerable<AgendaDTO>>(query);

            return Ok(horarios);
        }
    }
}

