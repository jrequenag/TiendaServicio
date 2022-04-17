using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

using TiendaServicio.Api.Libro.Modelo;
using TiendaServicio.Api.Libro.Persistencia;

namespace TiendaServicio.Api.Libro.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Titulo { get; set; }
            public DateTime? FechaPublicacion { get; set; }
            public Guid AutorLibroId { get; set; }

        }
        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.AutorLibroId).NotEmpty()
                    .NotNull();
                RuleFor(x=> x.Titulo).NotEmpty()
                    .NotNull();
                RuleFor(x => x.FechaPublicacion).NotEmpty();


            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly ContextoLibreria _contexto;
            private readonly IMapper _mapper;

            public Manejador(ContextoLibreria contexto
                , IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                
                await _contexto.LibreriaMaterial.AddAsync(_mapper.Map<Ejecuta, LibreriaMaterial>(request));
                var valor = await _contexto.SaveChangesAsync();
                if (valor > 0)
                    return Unit.Value;

                throw new Exception("No se pudo insertar el Libro material");
            }
        }
    }
}
