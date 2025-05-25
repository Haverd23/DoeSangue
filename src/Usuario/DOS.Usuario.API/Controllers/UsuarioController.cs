using DOS.Core.Mediator.Commands;
using DOS.Usuario.Application.Commands;
using DOS.Usuario.Domain;
using Microsoft.AspNetCore.Mvc;

namespace DOS.Usuario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ICommandDispatcher _commandDispatcher;

        public UsuarioController(IUsuarioRepository usuarioRepository,
            ICommandDispatcher commandDispatcher)
        {
            _usuarioRepository = usuarioRepository;
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> CriarUsuario([FromBody] UsuarioCriadoCommand request)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == "email")?.Value;

            if (string.IsNullOrEmpty(email))
                return Unauthorized("Email não encontrado no token.");

            request.SetEmail(email); 

            var usuarioId = await _commandDispatcher
                .DispatchAsync<UsuarioCriadoCommand, Guid>(request);

            return CreatedAtAction(nameof(CriarUsuario), new { id = usuarioId }, usuarioId);
        }
    }
}
