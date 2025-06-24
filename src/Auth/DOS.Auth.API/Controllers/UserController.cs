using DOS.Auth.API.DTOs;
using DOS.Auth.Application.Commands;
using DOS.Auth.Application.Services.Interfaces;
using DOS.Auth.Domain.Models;
using DOS.Core.Mediator.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DOS.Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly ILoginService _loginService;

        public UserController(ICommandDispatcher commandDispatcher, ILoginService loginService)
        {
            _commandDispatcher = commandDispatcher;
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> CriarUsuario([FromBody] CriarUserDTO request)
        {
            var command = new UsuarioCriadoCommand(request.Email, request.Senha);

            var usuarioId = await _commandDispatcher
                .DispatchAsync<UsuarioCriadoCommand, Guid>(command);

            return CreatedAtAction(nameof(CriarUsuario), new { id = usuarioId }, usuarioId);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO request)
        {
            var email = new Email(request.Email);
            var token = await _loginService.Autenticar(email, request.Senha);
            return Ok(token);
        }
        [HttpPut("alterar/senha")]
        public async Task<IActionResult> AlterarSenha([FromBody] AlterarSenhaDTO request)
        {
            var idString = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(idString, out Guid userId))
            {
                return Unauthorized("Id do usuário inválido no token.");
            }
            var command = new AlterarSenhaCommand(userId, request.Senha);
            var commandDispatcher = await _commandDispatcher.DispatchAsync<AlterarSenhaCommand,bool>(command);
            return NoContent();
        }
        [HttpPut("alterar/email")]
        public async Task<IActionResult> AlterarEmail([FromBody] AlterarEmailDTO request)
        {
            var idString = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(idString, out Guid userId))
            {
                return Unauthorized("Id do usuário inválido no token.");
            }
            var command = new AlterarEmailCommand(userId, request.Email);
            var commandDispatcher = await _commandDispatcher.DispatchAsync<AlterarEmailCommand, bool>(command);
            return NoContent();
        }
    }
}



