﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using TiendaServicio.Api.Libro.Aplicacion;
using TiendaServicio.Api.Libro.Modelo;

namespace TiendaServicio.Api.Libro.Test
{
    public class MappingTest : Profile
    {
        public MappingTest()
        {
            CreateMap<LibreriaMaterial, LibreriaMaterialDto>().ReverseMap();
            CreateMap<LibreriaMaterial, Nuevo.Ejecuta>()
               .ReverseMap();
        }

    }
}
