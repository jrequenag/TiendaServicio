using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using TiendaServicio.Api.CarritoCompra.Aplicaciones;

namespace TiendaServicio.Api.CarritoCompra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoComprasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarritoComprasController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult<Unit>> Agregar(Nuevo.Ejecuta request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CarritoDto>> GetCarrito(int id)
        {
            return await _mediator.Send(new Consulta.Ejecuta { CarritoSesionId = id });
        }
    }
}
