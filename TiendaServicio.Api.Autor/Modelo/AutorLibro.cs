﻿namespace TiendaServicio.Api.Autor.Modelo
{
    public class AutorLibro
    {
        public int AutorLibroId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public virtual ICollection<GradoAcademico> ListaGradoAcademico { get; set; }

        public string AutorLibroGuid { get; set; }

    }
}
