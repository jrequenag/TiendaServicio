using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaServicio.Api.Autor.Migrations
{
    public partial class FixCamelCase2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AuthorLibroGuid",
                table: "AutorLibros",
                newName: "AutorLibroGuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AutorLibroGuid",
                table: "AutorLibros",
                newName: "AuthorLibroGuid");
        }
    }
}
