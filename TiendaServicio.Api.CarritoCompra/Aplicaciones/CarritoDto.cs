using System;
using System.Collections.Generic;

namespace TiendaServicio.Api.CarritoCompra.Aplicaciones
{
    public class CarritoDto
    {
        public int CarritoId { get; set; }
        public DateTime? FechaCreacionSesion { get; set; }
        public List<CarritoDetalleDto> ListaProductos { get; set; }
    }
}
