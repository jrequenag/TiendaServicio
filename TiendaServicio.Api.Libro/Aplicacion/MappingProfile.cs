
using TiendaServicio.Api.Libro.Modelo;

namespace TiendaServicio.Api.Libro.Aplicacion
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LibreriaMaterial, Nuevo.Ejecuta>()
                .ReverseMap();
            CreateMap<LibreriaMaterial, LibreriaMaterialDto>().ReverseMap();
        }
    }
}
