
using Microsoft.AspNetCore.Mvc;

using TiendaServicio.Api.Libro.Aplicacion;

namespace TiendaServicio.Api.Libro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LibroController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<IList<LibreriaMaterialDto>>> GetAutores()
        {
            return Ok(await _mediator.Send(new Consulta.Ejecuta()));
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<LibreriaMaterialDto>> GetLibro(Guid Id)
        {
            return Ok(await _mediator.Send(new ConsultaFiltro.LibroUnico() { LibroId = Id }));
        }
        [HttpPost]
        public async Task<Unit> Agregar(Nuevo.Ejecuta request)
        {
            return await _mediator.Send(request);
        }
    }
}
