using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using TiendaServicio.Api.Libro.Modelo;
using TiendaServicio.Api.Libro.Persistencia;

namespace TiendaServicio.Api.Libro.Aplicacion
{
    public class Consulta
    {
        public class  Ejecuta : IRequest<IList<LibreriaMaterialDto>> { }
        public class Manejador : IRequestHandler<Ejecuta, IList<LibreriaMaterialDto>>
        {
            private readonly ContextoLibreria _contexto;
            private readonly IMapper _mapper;

            public Manejador(ContextoLibreria contexto
                , IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }

            public async Task<IList<LibreriaMaterialDto>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                return _mapper.Map<IList<LibreriaMaterial>, IList<LibreriaMaterialDto>>(await _contexto.LibreriaMaterial.ToListAsync());
            }
        }
    }
}
