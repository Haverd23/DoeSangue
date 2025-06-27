
using DOS.Core.Mediator.Commands;
using DOS.Core.Mediator.Queries;
using DOS.Estoque.API.DTOs;
using DOS.Estoque.Application.Commands;
using DOS.Estoque.Application.DTOs;
using DOS.Estoque.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DOS.Estoque.API.Controllers
{
    [Authorize(Roles = "Administrador")]
    [Route("api/[controller]")]
    [ApiController]
    public class EstoqueController : ControllerBase
    {
       private readonly IQueryDispatcher _queryDispatcher;
       private readonly ICommandDispatcher _commandDispatcher;

        public EstoqueController(IQueryDispatcher queryDispatcher,
            ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        [HttpGet]
        public async Task<IActionResult> ListarEstoque()
        {
            var query = new ListarEstoqueQuery();
            var estoque = await _queryDispatcher.DispatchAsync<ListarEstoqueQuery, IEnumerable<EstoqueDTO>>(query);
            return Ok(estoque);

        }
        [HttpPut]
        public async Task<IActionResult> RetirarUnidadeSanguinea(RetirarUnidadeSanguineaDTO
            request)
        {
            if (request == null)
            {
                return BadRequest("Informe o tipo sanguíneo e a quantidade que será retirado");
            }
            var command = new RetirarUnidadeSanguineaCommand
                (request.TipoSanguineo, request.Quantidade);

            var estoque = await _commandDispatcher.
                DispatchAsync<RetirarUnidadeSanguineaCommand,bool>(command);

            return NoContent();
        }
    }
}
