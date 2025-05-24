using DOS.Auth.API.DTOs;
using DOS.Auth.Application.Commands;
using DOS.Auth.Application.Services.Interfaces;
using DOS.Auth.Domain.Models;
using DOS.Core.Mediator.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var email = new Email(request.Email);
            var command = new UsuarioCriadoCommand(email, request.Senha);

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
    }

}

