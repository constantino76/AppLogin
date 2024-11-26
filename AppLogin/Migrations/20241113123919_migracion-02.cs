using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppLogin.Migrations
{
    /// <inheritdoc />
    public partial class migracion02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_Roles",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Roles", x => x.IdRol);
                });

            migrationBuilder.CreateTable(
                name: "tb_Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCompleto = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Correo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Clave = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Usuarios", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioRol",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdRol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioRol", x => new { x.IdUsuario, x.IdRol });
                    table.ForeignKey(
                        name: "FK_UsuarioRol_tb_Roles_IdRol",
                        column: x => x.IdRol,
                        principalTable: "tb_Roles",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioRol_tb_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "tb_Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRol_IdRol",
                table: "UsuarioRol",
                column: "IdRol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioRol");

            migrationBuilder.DropTable(
                name: "tb_Roles");

            migrationBuilder.DropTable(
                name: "tb_Usuarios");
        }
    }
}
