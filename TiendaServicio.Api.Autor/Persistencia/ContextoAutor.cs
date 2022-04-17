
using Microsoft.EntityFrameworkCore;

using TiendaServicio.Api.Autor.Modelo;

namespace TiendaServicio.Api.Autor.Persistencia
{
    public class ContextoAutor : DbContext
    {
        public ContextoAutor(DbContextOptions<ContextoAutor> contextOptions) : base(contextOptions)
        {

        }
        public DbSet<AutorLibro> AutorLibros { get; set; }
        public DbSet<GradoAcademico> GradoAcademico { get; set; }
    }
}
