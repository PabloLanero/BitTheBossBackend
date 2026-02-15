using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Partidas",
                columns: table => new
                {
                    IdPartida = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partidas", x => x.IdPartida);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tiers",
                columns: table => new
                {
                    IdTier = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Titulo = table.Column<string>(type: "longtext", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValue: new DateTime(2026, 2, 15, 22, 12, 12, 651, DateTimeKind.Local).AddTicks(6623))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tiers", x => x.IdTier);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false),
                    Correo = table.Column<string>(type: "longtext", nullable: false),
                    Password = table.Column<string>(type: "longtext", nullable: false),
                    Visible = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Usuario_Tier = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Tiers_Usuario_Tier",
                        column: x => x.Usuario_Tier,
                        principalTable: "Tiers",
                        principalColumn: "IdTier",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Nodo",
                columns: table => new
                {
                    IdNodo = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    DuenoNodoId = table.Column<int>(type: "int", nullable: true),
                    PartidaIdPartida = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nodo", x => x.IdNodo);
                    table.ForeignKey(
                        name: "FK_Nodo_Partidas_PartidaIdPartida",
                        column: x => x.PartidaIdPartida,
                        principalTable: "Partidas",
                        principalColumn: "IdPartida");
                    table.ForeignKey(
                        name: "FK_Nodo_Usuarios_DuenoNodoId",
                        column: x => x.DuenoNodoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PartidaUsuario",
                columns: table => new
                {
                    ArrUsuarioId = table.Column<int>(type: "int", nullable: false),
                    LstPartidasIdPartida = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartidaUsuario", x => new { x.ArrUsuarioId, x.LstPartidasIdPartida });
                    table.ForeignKey(
                        name: "FK_PartidaUsuario_Partidas_LstPartidasIdPartida",
                        column: x => x.LstPartidasIdPartida,
                        principalTable: "Partidas",
                        principalColumn: "IdPartida",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartidaUsuario_Usuarios_ArrUsuarioId",
                        column: x => x.ArrUsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tropa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false),
                    Vida = table.Column<float>(type: "float", nullable: false),
                    Damage = table.Column<float>(type: "float", nullable: false),
                    NodoIdNodo = table.Column<byte>(type: "tinyint unsigned", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tropa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tropa_Nodo_NodoIdNodo",
                        column: x => x.NodoIdNodo,
                        principalTable: "Nodo",
                        principalColumn: "IdNodo");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Tiers",
                columns: new[] { "IdTier", "FechaCreacion", "Titulo" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 2, 15, 22, 12, 12, 651, DateTimeKind.Local).AddTicks(6270), "Bronce" },
                    { 2, new DateTime(2026, 2, 15, 22, 12, 12, 651, DateTimeKind.Local).AddTicks(6273), "Plata" },
                    { 3, new DateTime(2026, 2, 15, 22, 12, 12, 651, DateTimeKind.Local).AddTicks(6275), "Oro" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nodo_DuenoNodoId",
                table: "Nodo",
                column: "DuenoNodoId");

            migrationBuilder.CreateIndex(
                name: "IX_Nodo_PartidaIdPartida",
                table: "Nodo",
                column: "PartidaIdPartida");

            migrationBuilder.CreateIndex(
                name: "IX_PartidaUsuario_LstPartidasIdPartida",
                table: "PartidaUsuario",
                column: "LstPartidasIdPartida");

            migrationBuilder.CreateIndex(
                name: "IX_Tropa_NodoIdNodo",
                table: "Tropa",
                column: "NodoIdNodo");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Usuario_Tier",
                table: "Usuarios",
                column: "Usuario_Tier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartidaUsuario");

            migrationBuilder.DropTable(
                name: "Tropa");

            migrationBuilder.DropTable(
                name: "Nodo");

            migrationBuilder.DropTable(
                name: "Partidas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Tiers");
        }
    }
}
