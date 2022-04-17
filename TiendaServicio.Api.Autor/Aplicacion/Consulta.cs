
using Microsoft.EntityFrameworkCore;

using TiendaServicio.Api.Autor.Modelo;
using TiendaServicio.Api.Autor.Persistencia;

namespace TiendaServicio.Api.Autor.Aplicacion
{
    public class Consulta
    {
        public class ListaAutor : IRequest<IList<AutorDto>>
        {

        }

        public class Manejador : IRequestHandler<ListaAutor, IList<AutorDto>>
        {
            private readonly ContextoAutor _contexto;
            private readonly IMapper _mapper;

            public Manejador(ContextoAutor contexto
                , IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }
            public async Task<IList<AutorDto>> Handle(ListaAutor request, CancellationToken cancellationToken)
            {
                return _mapper.Map<IList<AutorLibro>, IList<AutorDto>>(await _contexto.AutorLibros.ToListAsync());
            }
        }
    }
}
