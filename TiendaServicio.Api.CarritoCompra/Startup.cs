global using AutoMapper;

global using MediatR;

using FluentValidation.AspNetCore;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

using TiendaServicio.Api.CarritoCompra.Persistencia;
using TiendaServicio.Api.CarritoCompra.RemoteInterface;
using TiendaServicio.Api.CarritoCompra.RemoteService;

namespace TiendaServicio.Api.CarritoCompra
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<CarritoContext>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString("ConexionDataBase"));
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TiendaServicio.Api.CarritoCompra", Version = "v1" });
            });
            services.AddAutoMapper(System.Reflection.Assembly.GetExecutingAssembly());
            services.AddMediatR(System.Reflection.Assembly.GetExecutingAssembly());


            services.AddHttpClient("Libros", config =>
            {
                config.BaseAddress = new Uri(Configuration["Services:Libros"]);
            });

            services.AddScoped<ILibroService, LibroService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TiendaServicio.Api.CarritoCompra v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
