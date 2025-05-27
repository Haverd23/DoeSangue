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
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdClaim.Value, out var userId))
                return BadRequest("UserId inválido nas claims");

            var command = new AgendarDoacaoCommand(request.AgendaId, request.DataHoraAgendada);
            command.UserId = userId;
            var doacaoId = await _commandDispatcher.DispatchAsync<AgendarDoacaoCommand, Guid>(command);
            return CreatedAtAction(nameof(Agendar), new { id = doacaoId }, doacaoId);
        }
    }
}

