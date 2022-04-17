using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TiendaServicio.Api.Autor.Modelo;

namespace TiendaServicio.Api.Autor.Aplicacion
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AutorLibro, AutorDto>()
              
                .ReverseMap();
        }
    }
}
