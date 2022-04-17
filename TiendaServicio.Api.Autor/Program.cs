global using AutoMapper;

global using MediatR;

using FluentValidation.AspNetCore;

using Microsoft.EntityFrameworkCore;

using TiendaServicio.Api.Autor.Persistencia;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(cfg =>
{
    cfg.RegisterValidatorsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
});
builder.Services.AddMediatR(System.Reflection.Assembly.GetExecutingAssembly());

builder.Services.AddDbContext<ContextoAutor>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("ConexionDataBase"));
});

builder.Services.AddAutoMapper(System.Reflection.Assembly.GetExecutingAssembly());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
