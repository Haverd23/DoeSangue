
using DOS.Core.Mediator.Queries;
using DOS.Estoque.Application.DTOs;
using DOS.Estoque.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DOS.Estoque.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EstoqueController : ControllerBase
    {
       private readonly IQueryDispatcher _queryDispatcher;

        public EstoqueController(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        public async Task<IActionResult> ListarEstoque()
        {
            var query = new ListarEstoqueQuery();
            var estoque = await _queryDispatcher.DispatchAsync<ListarEstoqueQuery, IEnumerable<EstoqueDTO>>(query);
            return Ok(estoque);

        }
    }
}
