using DOS.Core.Mediator.Commands;
using DOS.Estoque.Application.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DOS.Estoque.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstoqueController : ControllerBase
    {
        private readonly ICommandDispatcher _comandDispatcher;
        public EstoqueController(ICommandDispatcher comandDispatcher)
        {
            _comandDispatcher = comandDispatcher;
        }
        [HttpPost]
        public async Task<IActionResult> Estoque([FromBody] RegistrarDoacaoEstoqueCommand estoque)
        {
            var result = await _comandDispatcher.DispatchAsync<RegistrarDoacaoEstoqueCommand, bool>(estoque);
            if (result)
            {
                return Ok("Doação registrada com sucesso.");
            }
            else
            {
                return BadRequest("Erro ao registrar a doação.");
            }
        }
    }
}
