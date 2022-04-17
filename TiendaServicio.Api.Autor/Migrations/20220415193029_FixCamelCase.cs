using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaServicio.Api.Autor.Migrations
{
    public partial class FixCamelCase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AuthorLIbroGuid",
                table: "AutorLibros",
                newName: "AuthorLibroGuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AuthorLibroGuid",
                table: "AutorLibros",
                newName: "AuthorLIbroGuid");
        }
    }
}
