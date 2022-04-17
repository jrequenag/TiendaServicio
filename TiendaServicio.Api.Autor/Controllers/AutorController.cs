
using Microsoft.AspNetCore.Mvc;

using TiendaServicio.Api.Autor.Aplicacion;
using TiendaServicio.Api.Autor.Modelo;

namespace TiendaServicio.Api.Autor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AutorController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<IList<AutorDto>>> GetAutores()
        {
            return Ok(await _mediator.Send(new Consulta.ListaAutor()));
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<AutorDto>> GetAutor(string Id)
        {
            return Ok(await _mediator.Send(new ConsultaFiltro.AutorUnico() { AutorGuid = Id }));
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await _mediator.Send(data);
        }
    }
}
