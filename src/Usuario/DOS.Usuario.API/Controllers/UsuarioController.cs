using DOS.Core.Mediator.Commands;
using DOS.Core.Mediator.Queries;
using DOS.Usuario.API.DTOs;
using DOS.Usuario.Application.Commands;
using DOS.Usuario.Application.DTOs;
using DOS.Usuario.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DOS.Usuario.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public UsuarioController(ICommandDispatcher commandDispatcher,
            IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> CriarUsuario([FromBody] CriarUsuarioDTO dto)
        {

            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var idString = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(idString, out Guid userId))
            {
                return Unauthorized("Id do usuário inválido no token.");
            }
            if (string.IsNullOrEmpty(email))
                return Unauthorized("Email não encontrado no token.");

            var command = new UsuarioCriadoCommand(dto.Nome, dto.CPF, dto.Telefone, dto.TipoSanguineo);
            command.SetEmail(email);
            command.SetId(userId);

            var usuarioId = await _commandDispatcher.DispatchAsync<UsuarioCriadoCommand, Guid>(command);

            return CreatedAtAction(nameof(CriarUsuario), new { id = usuarioId }, usuarioId);
        }
        [HttpPut("alterar/telefone")]
        public async Task<IActionResult> AlterarTelefone([FromBody] AlterarTelefoneDTO dto)
        {
            var idString = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(idString, out Guid userId))
            {
                return Unauthorized("Id do usuário inválido no token.");
            }
            var command = new AlterarTelefoneCommand(dto.Telefone, userId);
            var commandDispatcher = await _commandDispatcher.DispatchAsync<AlterarTelefoneCommand,bool>(command);
            return NoContent();

        }
        [HttpPut("alterar/tiposanguineo")]
        public async Task<IActionResult> AlterarTipoSanguineo([FromBody] AlterarTipoSanguineoDTO dto)
        {
            var command = new AlterarTipoSanguineoCommand(dto.CPF, dto.TipoSanguineo);
            var commandDispatcher = await _commandDispatcher.DispatchAsync<AlterarTipoSanguineoCommand, bool>(command);
            return NoContent();
        }
        [HttpGet("doacoes")]
        public async Task<IActionResult> HistoricoDoacoes()
        {
            var idString = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(idString, out Guid userId))
            {
                return Unauthorized("Id do usuário inválido no token.");
            }
            var query = new DoacaoHistoricoQuery(userId);
            var queryDispatcher = await _queryDispatcher.DispatchAsync<DoacaoHistoricoQuery,
                IEnumerable<HistoricoDoacaoDTO>>(query);

            return Ok(queryDispatcher);

        }
    }
}
