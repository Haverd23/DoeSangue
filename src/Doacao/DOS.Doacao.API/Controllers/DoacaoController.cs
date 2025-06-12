using DOS.Core.Mediator.Commands;
using DOS.Doacao.API.DTOs;
using DOS.Doacao.Application.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DOS.Doacao.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class DoacaoController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public DoacaoController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }
        [HttpPost("agendar")]
        public async Task<IActionResult> Agendar([FromBody] AgendarDoacaoDTO request)
        {
            var idString = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(idString, out Guid userId))
            {
                return Unauthorized("Id do usuário inválido no token.");
            }


            var command = new AgendarDoacaoCommand(request.AgendaId, request.DataHoraAgendada);
            command.UserId = userId;
            var doacaoId = await _commandDispatcher.DispatchAsync<AgendarDoacaoCommand, Guid>(command);
            return CreatedAtAction(nameof(Agendar), new { id = doacaoId }, doacaoId);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost("iniciar")]
        public async Task<IActionResult> Iniciar([FromBody] DoacaoRealizadaDTO request)
        {
            var command = new DoacaoRealizadaCommand(request.DoacaoId);
            var doacaoId = await _commandDispatcher.DispatchAsync<DoacaoRealizadaCommand, bool>(command);
            return NoContent();
        }
        [Authorize(Roles = "Administrador")]
        [HttpPost("finalizar")]
        public async Task<IActionResult> Finalizar([FromBody] DoacaoFinalizadaDTO request)
        {
            var command = new DoacaoFinalizadaCommand(request.DoacaoId);
            var doacaoId = await _commandDispatcher.DispatchAsync<DoacaoFinalizadaCommand, bool>(command);
            return NoContent();
        }
        [Authorize(Roles = "Administrador")]
        [HttpPost("falha")]
        public async Task<IActionResult> Invalidar([FromBody] DoacaoFalhaDTO request)
        {
            var command = new DoacaoFalhaCommand(request.DoacaoId);
            var doacaoId = await _commandDispatcher.DispatchAsync<DoacaoFalhaCommand,bool>(command);
            return NoContent();
        }
    }
}

