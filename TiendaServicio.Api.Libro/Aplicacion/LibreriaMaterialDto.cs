using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaServicio.Api.Libro.Aplicacion
{
    public class LibreriaMaterialDto
    {
        public Guid? LibreriaMaterialId { get; set; }
        public string Titulo { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public Guid AutorLibroId { get; set; }

    }
}
