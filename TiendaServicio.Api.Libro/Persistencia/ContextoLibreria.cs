using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using TiendaServicio.Api.Libro.Modelo;

namespace TiendaServicio.Api.Libro.Persistencia
{
    public class ContextoLibreria : DbContext
    {
        public ContextoLibreria()
        {

        }
        public ContextoLibreria(DbContextOptions<ContextoLibreria> contextOptions) : base(contextOptions)
        {

        }
        public virtual DbSet<LibreriaMaterial> LibreriaMaterial { get; set; }
    }
}
