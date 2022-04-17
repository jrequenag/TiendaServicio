using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using TiendaServicio.Api.CarritoCompra.Modelo;
using TiendaServicio.Api.CarritoCompra.Persistencia;

namespace TiendaServicio.Api.CarritoCompra.Aplicaciones
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public DateTime FechaCreacion { get; set; }
            public List<string> ProductoLista { get; set; }
        }
        //public class EjecutaValidacion : AbstractValidator<Ejecuta>
        //{
        //    public EjecutaValidacion()
        //    {
        //        RuleFor(x => x.AutorLibroId).NotEmpty();
        //        RuleFor(x => x.Titulo).NotEmpty();
        //        RuleFor(x => x.FechaPublicacion).NotEmpty();


        //    }
        //}

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly CarritoContext _contexto;
            private readonly IMapper _mapper;

            public Manejador(CarritoContext contexto
                , IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {

                var carritoSesion = new CarritoSesion()
                {
                    FechaCreacion = request.FechaCreacion
                };
                await _contexto.CarritoSesion.AddAsync(carritoSesion);
                var valor = await _contexto.SaveChangesAsync();
                if(valor == 0)
                    throw new Exception("No se pudo insertar el detalle de compra"); 
                foreach (var item in request.ProductoLista)
                {
                    var detalle = new CarritoSesionDetalle()
                    {
                        CarritoSesionId = carritoSesion.CarritoSesionId,
                        FechaCreacion = DateTime.Now,
                        ProductoSeleccionado = item
                    };
                    await _contexto.CarritoSesionDetalle.AddAsync(detalle);
                }
                valor = await _contexto.SaveChangesAsync();
                if (valor > 0)
                    return Unit.Value;
                throw new Exception("No se pudo insertar el detalle de compra");
            }
        }
    }
}
