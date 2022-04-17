using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using GenFu;

using MediatR;

using Microsoft.EntityFrameworkCore;

using Moq;

using TiendaServicio.Api.Libro.Aplicacion;
using TiendaServicio.Api.Libro.Modelo;
using TiendaServicio.Api.Libro.Persistencia;

using Xunit;

namespace TiendaServicio.Api.Libro.Test
{
    public class LibrosServiceTest
    {

        private IEnumerable<LibreriaMaterial> ObtenerDataPrueba()
        {

            //Generador de datos de prueba con GenFu;
            A.Configure<LibreriaMaterial>()
                .Fill(x=> x.Titulo).AsArticleTitle()
                .Fill(x=> x.LibreriaMaterialId, () => { return Guid.NewGuid(); });

            var list = A.ListOf<LibreriaMaterial>(30);

            list[0].LibreriaMaterialId = Guid.Empty;
            return list;
        }

        private Mock<ContextoLibreria> CrearContexto()
        {
            //Con esto y las clases AsyncEnumerable y AsyncEnumerator, se enula la instanacia para obtener los datos
            // de entity de manera asincrona
            var dataPrueba = ObtenerDataPrueba().AsQueryable();
            var dbSet = new Mock<DbSet<LibreriaMaterial>>();

            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(dataPrueba.Provider);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Expression).Returns(dataPrueba.Expression);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.ElementType).Returns(dataPrueba.ElementType);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.GetEnumerator()).Returns(dataPrueba.GetEnumerator());


            dbSet.As<IAsyncEnumerable<LibreriaMaterial>>().Setup(x => x.GetAsyncEnumerator(new System.Threading.CancellationToken()))
                .Returns(new AsyncEnumerator<LibreriaMaterial>(dataPrueba.GetEnumerator()));

            //Para filtrar por uno
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(new AsyncQueryProvider<LibreriaMaterial>(dataPrueba.Provider));


            var contextoLibreria = new Mock<ContextoLibreria>();
            contextoLibreria.Setup(x => x.LibreriaMaterial).Returns(dbSet.Object);
            return contextoLibreria;

        }

        [Fact]
        public async void GetLibros()
        {
            var mockContexto = CrearContexto();
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });
            var mapper = mapConfig.CreateMapper();

            var manejador = new Consulta.Manejador(mockContexto.Object, mapper);
            var request = new Consulta.Ejecuta();

            var lista = await manejador.Handle(request, new System.Threading.CancellationToken());

            Assert.True(lista.Any());

        }

        [Fact]
        public async void GetLibro()
        {
            var mockContexto = CrearContexto();
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });
            var mapper = mapConfig.CreateMapper();

            var manejador = new ConsultaFiltro.Manejador(mockContexto.Object, mapper);
            var request = new ConsultaFiltro.LibroUnico()
            {
                LibroId = Guid.Empty
            };

            var libro = await manejador.Handle(request, new System.Threading.CancellationToken());

            Assert.NotNull(libro);
            Assert.True(libro.LibreriaMaterialId == Guid.Empty);

        }
        [Fact]
        public async void GetLibroNoFound()
        {
            var mockContexto = CrearContexto();
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });
            var mapper = mapConfig.CreateMapper();

            var manejador = new ConsultaFiltro.Manejador(mockContexto.Object, mapper);
            var request = new ConsultaFiltro.LibroUnico()
            {
                LibroId = Guid.NewGuid()
            };
            try
            {
                var libro = await manejador.Handle(request, new System.Threading.CancellationToken());
            }
            catch (Exception ex)
            {

                Assert.Contains("No se encontro", ex.Message);
            }

            

            

        }
        [Fact]
        public async void GuardarLibro()
        {
            var options = new DbContextOptionsBuilder<ContextoLibreria>()
                .UseInMemoryDatabase(databaseName: "BaseDatosLibro")
                .Options;
            var contexto = new ContextoLibreria(options);
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });
            var mapper = mapConfig.CreateMapper();
            var resquest = new Nuevo.Ejecuta()
            {
                Titulo = "Mi titulo prueba",
                AutorLibroId = Guid.Empty,
                FechaPublicacion = DateTime.Now
            };
            var manejador = new Nuevo.Manejador(contexto, mapper);
            var result = await manejador.Handle(resquest, new System.Threading.CancellationToken());
            Assert.Equal(Unit.Value, result);            
        }

    }
}
