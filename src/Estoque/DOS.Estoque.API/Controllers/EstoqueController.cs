
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DOS.Estoque.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EstoqueController : ControllerBase
    {
       
        
    }
}
