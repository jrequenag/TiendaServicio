using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using TiendaServicio.Api.CarritoCompra.Persistencia;
using TiendaServicio.Api.CarritoCompra.RemoteInterface;

namespace TiendaServicio.Api.CarritoCompra.Aplicaciones
{
    public class Consulta
    {
        public class Ejecuta : IRequest<CarritoDto>
        {
            public int CarritoSesionId { get; set; }

        }
        public class Manejador : IRequestHandler<Ejecuta, CarritoDto>
        {
            private readonly CarritoContext _context;
            private readonly ILibroService _libroService;

            public Manejador(CarritoContext context
                , ILibroService libroService)
            {
                _context = context;
                _libroService = libroService;
            }
            public async Task<CarritoDto> Handle(Ejecuta request, CancellationToken cancellationToken)
            {

                var listaCarritoDto = new List<CarritoDetalleDto>();

                var carritoSesion = await _context.CarritoSesion.FirstOrDefaultAsync(x => x.CarritoSesionId == request.CarritoSesionId);
                if (carritoSesion is null)
                    return null;

                var carritoSesionDetalle = await _context.CarritoSesionDetalle.Where(x => x.CarritoSesionId == request.CarritoSesionId).ToListAsync();
                if (carritoSesionDetalle is null)
                    return null;

                foreach (var libro in carritoSesionDetalle)
                {
                    var response = await _libroService.GetLibro(new Guid(libro.ProductoSeleccionado));
                    if (response.resultado)
                    {
                        var objetoLibro = response.Libro;
                        var carritoDetalle = new CarritoDetalleDto()
                        {
                            TituloLibro = objetoLibro.Titulo,
                            FechaPublicacion = objetoLibro.FechaPublicacion,
                            LibroId = objetoLibro.LibreriaMaterialId,
                        };
                        listaCarritoDto.Add(carritoDetalle);
                    }

                }

                var carritoSesionDto = new CarritoDto()
                {
                    CarritoId = carritoSesion.CarritoSesionId,
                    FechaCreacionSesion = carritoSesion.FechaCreacion,
                    ListaProductos = listaCarritoDto
                };
                return carritoSesionDto;
            }
        }
    }
}
