
using Microsoft.EntityFrameworkCore;

using TiendaServicio.Api.CarritoCompra.Modelo;

namespace TiendaServicio.Api.CarritoCompra.Persistencia
{
    public class CarritoContext : DbContext
    {
        public CarritoContext(DbContextOptions<CarritoContext> contextOptions) : base(contextOptions)
        {

        }

        public DbSet<CarritoSesion> CarritoSesion { get; set; }
        public DbSet<CarritoSesionDetalle> CarritoSesionDetalle { get; set; }
    }
}
