global using AutoMapper;

global using MediatR;

using FluentValidation.AspNetCore;

using Microsoft.EntityFrameworkCore;

using TiendaServicio.Api.Libro.Persistencia;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(cfg =>
{
    cfg.RegisterValidatorsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
});
builder.Services.AddMediatR(System.Reflection.Assembly.GetExecutingAssembly());

builder.Services.AddDbContext<ContextoLibreria>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ConexionDataBase"));
});

builder.Services.AddAutoMapper(System.Reflection.Assembly.GetExecutingAssembly());
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
